using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//--
using Microsoft.Win32;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace study1_manager_gui
{
    static partial class ParticipantDatabase
    {
        public static string databasePath = @"";

        internal const string REG_SUBKEY = @"SOFTWARE\ben\study1";
        internal const string REG_VALUE_DBPATH = @"databasePath";

        public static RegistryKey registry;

        private static Excel.Application app;
        private static Excel.Workbook workbook;
        private static Excel._Worksheet worksheet;
        private static Excel.Range range;

        public static List<Participant> participants = new List<Participant>();

        public static bool tryLoadDatabase()
        {
            registry = Registry.CurrentUser.OpenSubKey(REG_SUBKEY, true);

            if (registry == null)
                return false;

            if (registry != null)
                databasePath = (string) (registry.GetValue(REG_VALUE_DBPATH) ?? "");

            if (databasePath.Equals(""))
                return false;

            loadDatabase();
            return true;
        }

        private static void loadDatabase()
        {
            object _missingValue = System.Reflection.Missing.Value;
            app = new Microsoft.Office.Interop.Excel.Application();
            workbook = app.Workbooks.Open(databasePath,
                                                _missingValue,
                                                false,
                                                _missingValue,
                                                _missingValue,
                                                _missingValue,
                                                true,
                                                _missingValue,
                                                _missingValue,
                                                true,
                                                _missingValue,
                                                _missingValue,
                                                _missingValue);

            worksheet = app.Worksheets[1];
            range = worksheet.UsedRange;

            var notes = readColumn(1);
            var emails = readColumn(2);
            var realIDs = readColumn(3);
            var IDs = readColumn(4);
            var groups = readColumn(5);
            var s1 = readColumn(6);
            var s2 = readColumn(7);
            var s3 = readColumn(8);

            for(int i = 0; i < notes.Count; i++)
            {
                var participant = new Participant
                {
                    email = emails[i],
                    group = groups[i],
                    ID = IDs[i],
                    realID = int.Parse(realIDs[i]),
                    notes = notes[i],
                    sessionCompletion = new int[]
                    {
                        int.Parse(s1[i]),
                        int.Parse(s2[i]),
                        int.Parse(s3[i])
                    }
                };

                participants.Add(participant);
            }
            

        }

        public static Participant findByID(string id)
        {
            return participants.FirstOrDefault(x => x.ID.ToLower().Equals(id.ToLower()));
        }

        public static Participant find(string notes)
        {
            return participants.FirstOrDefault(x => x.notes.ToLower().Equals(notes.ToLower()));
        }

        private static List<string> readColumn(int index)
        {
            Excel.Range column = range.Columns[index];
            var values = (Array)column.Cells.Value;

            return values.Cast<object>().Skip(2).Select(x => (x == null) ? ("") : x.ToString()).ToList();
        }

        public static void close()
        {
            try
            {
                workbook.RefreshAll();
                app.Calculate();

                workbook.Close(true);
                app.Quit();

                Marshal.ReleaseComObject(workbook);
                Marshal.ReleaseComObject(range);
                Marshal.ReleaseComObject(worksheet);
                Marshal.ReleaseComObject(app);
            }
            catch(Exception e)
            {

            }
        }

        public static void writeDatabasePath(string input)
        {
            var key = Registry.CurrentUser.OpenSubKey(REG_SUBKEY, true);

            if (key == null)
            {
                registry = Registry.CurrentUser.CreateSubKey(REG_SUBKEY, true);
                registry.SetValue(REG_VALUE_DBPATH, input, RegistryValueKind.String);
            }
            else
                key.SetValue(REG_VALUE_DBPATH, input, RegistryValueKind.String);
        }

        internal static void setSession(Participant participant, int session)
        {
            //get column + row num
            var ids = readColumn(4);
            var rowNumber = ids.IndexOf(participant.ID) + 1;

            //set locally
            participant.sessionCompletion[session - 1] += 1;

            //set notes and email
            worksheet.Cells[rowNumber + 2, 5 + session] = "" + participant.sessionCompletion[session - 1];

            //do some stuff
            workbook.RefreshAll();
            app.Calculate();

            //save the sheet
            workbook.Save();
        }

        internal static void modifyDetails(Participant participant)
        {
            var ids = readColumn(4);
            var rowNumber = ids.IndexOf(participant.ID) + 1;

            //email is column 2, notes is column 1
            //..

            //set notes and email
            worksheet.Cells[rowNumber + 2, 1] = participant.notes;
            worksheet.Cells[rowNumber + 2, 2] = participant.email;

            //do some stuff
            workbook.RefreshAll();
            app.Calculate();

            //save the sheet
            workbook.Save();
        }
    }
}
