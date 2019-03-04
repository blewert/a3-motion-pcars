using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace study1_manager_gui
{
    public partial class mainForm : Form
    {
        private Form modifyForm;

        public mainForm()
        {
            InitializeComponent();

            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void newParticipantButton_Click(object sender, EventArgs e)
        {
            //find the first participant who is assigned a valid group
            //and hasn't completed s1, s2 or s3.
            //...

            Participant found = null;

            foreach(var participant in ParticipantDatabase.participants)
            {
                if (participant.group == "n/a" || participant.group == "")
                    continue;

                if (!string.IsNullOrWhiteSpace(participant.email))
                    continue;

                if(participant.sessionCompletion.All(x => x == 0))
                {
                    found = participant;
                    break;
                }
            }

            if (found == null)
            {
                MessageBox.Show("Couldn't find a participant ID to use.", "Error");
                return;
            }

            loadParticipant(found);
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            modifyForm = new modifyUserData();
            modifyForm.Hide();

            //Hide panel
            showPanel(false);

            //Load the database
            loadDatabase();
        }

        private void loadDatabase()
        {
            if (!ParticipantDatabase.tryLoadDatabase())
            {
                MessageBox.Show("The participant spreadsheet file has not been located. Please select it in the following dialog.", "Database file not found", MessageBoxButtons.OK, MessageBoxIcon.Information);

                var result = openFileDialog.ShowDialog();

                if (result != DialogResult.OK)
                {
                    MessageBox.Show("You didn't seem to select a file. Exiting..", "Invalid selection", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Application.Exit();
                }

                ParticipantDatabase.writeDatabasePath(openFileDialog.FileName);
                
            }

            //Otherwise, loading was fine..
            AutoCompleteStringCollection src = new AutoCompleteStringCollection();
            src.AddRange(ParticipantDatabase.participants.Where(x => !x.notes.Equals("")).Select(x => x.notes).ToArray());

            searchBox.AutoCompleteMode = participantIdBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            searchBox.AutoCompleteSource = participantIdBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            searchBox.AutoCompleteCustomSource = src;

            src = new AutoCompleteStringCollection();
            src.AddRange(ParticipantDatabase.participants.Where(x => !x.group.Equals("")).Select(x => x.ID).ToArray());
            participantIdBox.AutoCompleteCustomSource = src;
        }

        private void mainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

            ParticipantDatabase.close();
        }

        private Participant loaded;

        private void loadParticipant(Participant participant)
        {
            loaded = participant;

            searchTitleLabel.Text = "Participant " + participant.ID;

            var groupLabels = getGroupLabels(participant.group);

            if(groupLabels.Count() < 2)
            {
                s2CompletionButton.Hide();
                s3CompletionButton.Hide();
            }

            var groupString = string.Join(" -> ", groupLabels.ToArray());
            var completedSessions = getCompletedSessions(participant.sessionCompletion);

            searchInfoLabel.Text = "";
            searchInfoLabel.Text += "Group: " + groupString + " (" + participant.group + ")\n";
            searchInfoLabel.Text += "Completed sessions: " + string.Join(",", completedSessions) + "\n";
            searchInfoLabel.Text += "Real ID: " + participant.realID + "\n";
            searchInfoLabel.Text += "Name/Notes: " + participant.notes + "\n";
            searchInfoLabel.Text += "Email: " + participant.email;

            var controls = new List<Control>
            {
                s1CompletionButton,
                s2CompletionButton,
                s3CompletionButton
            };

            completionProgressBar.Value = 0;

            for(int i = 0; i < participant.sessionCompletion.Length; i++)
            {
                controls[i].Enabled = true;

                if(i >= groupLabels.Count)
                    controls[i].Text = "SESSION #" + (i + 1) + " (normal)";
                else
                    controls[i].Text = "SESSION #" + (i + 1) + " (" + groupLabels[i] + ")";

                if (participant.sessionCompletion[i] >= 1)
                {
                    //controls[i].Enabled = false;
                    controls[i].ForeColor = Color.OrangeRed;
                    completionProgressBar.Increment(33);
                }
            }

            showPanel(true);
        }

        private List<int> getCompletedSessions(int[] status)
        {
            var ret = new List<int>();

            for(int i = 0; i < status.Count(); i++)
            {
                if (status[i] >= 1)
                    ret.Add(i + 1);
            }

            return ret;
        }

        private List<string> getGroupLabels(string group)
        {
            var list = new List<string>();

            if(group.Equals("n/a"))
                return new List<string> { "n/a", "n/a", "n/a" };

            if(group.Equals("F"))
                return new List<string> { "fans", "fans", "fans" };

            if(group.Equals("NF"))
                return new List<string> { "no fans", "no fans", "no fans" };

            foreach (char c in group)
            {
                if (c == 'm') list.Add("normal");
                if (c == 'i') list.Add("inverted");
                if (c == 'n') list.Add("none");
            }

            return list;
        }

        private void showPanel(bool show)
        {
            foreach(Control control in resultsPanel.Controls)
            {
                if (show)
                    control.Show();
                else
                    control.Hide();
            }                
        }

        private void showButton_Click(object sender, EventArgs e)
        {
            var participant = ParticipantDatabase.find(searchBox.Text);

            if (!participantIdBox.Text.Equals(""))
                participant = ParticipantDatabase.findByID(participantIdBox.Text);

            if (participant == null)
            {
                MessageBox.Show("Couldn't find this participant.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            loadParticipant(participant);
        }

        private void searchBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Return)
                showButton_Click(sender, e);
        }

        private void dataModifyButton_Click(object sender, EventArgs e)
        {
            var form = modifyForm as modifyUserData;
            form.setParticipant(loaded, this);
            modifyForm.ShowDialog();            
        }

        public void saveParticipantDetails(Participant participant)
        {
            for(int i = 0; i < ParticipantDatabase.participants.Count; i++)
            {
                var p = ParticipantDatabase.participants[i];

                if (p.ID.ToLower() == participant.ID.ToLower())
                {
                    ParticipantDatabase.participants[i] = participant;
                    loadParticipant(participant);
                    ParticipantDatabase.modifyDetails(participant);
                    break;
                }
            }
        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

            ParticipantDatabase.close();
        }

        private void startInverter(int mode, bool practice = false)
        {
            if(ParticipantDatabase.registry.GetValue("inverterPath") == null)
            {
                MessageBox.Show("You have not yet selected a path for the inverter. Please locate it in the following dialog.", "Can't find inverter");

                var result = openFileDialogProcess.ShowDialog();

                if(result != DialogResult.OK)
                {
                    MessageBox.Show("You didn't seem to select a file. Try again.", "Error");
                    return;
                }

                ParticipantDatabase.registry.SetValue("inverterPath", openFileDialogProcess.FileName);
            }

            string[] flags = null;

            if (loaded != null)
            {
                flags = loaded.group.ToCharArray().Select(x => "-" + x).ToArray();

                //Set normal motion for n/a, NF, or F
                if (loaded.group == "n/a" || loaded.group == "NF" || loaded.group == "F")
                    flags[mode] = "-m";

                if (practice)
                    flags = new string[] { "-m", "-i", "-n" };
                
                if(!practice)
                    Process.Start((string)ParticipantDatabase.registry.GetValue("inverterPath"), flags[mode] + " " + loaded.ID);

                //No motion for practice!
                else
                    Process.Start((string)ParticipantDatabase.registry.GetValue("inverterPath"), flags[mode] + " practice yes");
            }
            else
            {
                MessageBox.Show("In order to run a practice lap, you have to select a participant. Use the search feature above.", "Error");
            }
            //Process.Start((string)ParticipantDatabase.registry.GetValue("inverterPath"), )
        }

        private void s1CompletionButton_Click(object sender, EventArgs e)
        {
            ParticipantDatabase.setSession(loaded, 1);
            startInverter(0);
            loadParticipant(loaded);
        }

        private void s2CompletionButton_Click(object sender, EventArgs e)
        {
            ParticipantDatabase.setSession(loaded, 2);
            startInverter(1);
            loadParticipant(loaded);
        }

        private void s3CompletionButton_Click(object sender, EventArgs e)
        {
            ParticipantDatabase.setSession(loaded, 3);
            startInverter(2);
            loadParticipant(loaded);
        }

        private void practiceLapButtonNormal_Click(object sender, EventArgs e)
        {
            startInverter(0, true);
        }

        private void practiceLapInverted_Click(object sender, EventArgs e)
        {
            startInverter(1, true);
        }

        private void practiceLapNone_Click(object sender, EventArgs e)
        {
            startInverter(2, true);
        }

        private void statusLabel_Click(object sender, EventArgs e)
        {

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Process[] processlist = Process.GetProcesses();

            bool isTelemetryRunning = processlist.Any(x => x.ProcessName.ToLower().Equals("project-cars-telemetry-writer"));
            bool isInverterRunning  = processlist.Any(x => x.ProcessName.ToLower().Equals("chair-inverter-udp"));

            if (isTelemetryRunning)
            {
                statusLabelRunning1.Text = "RUNNING";
                statusLabelRunning1.BackColor = Color.Green;
            }
            else
            {
                statusLabelRunning1.Text = "NOT RUNNING";
                statusLabelRunning1.BackColor = Color.Red;
            }

            if (isInverterRunning)
            {
                statusLabelRunning2.Text = "RUNNING";
                statusLabelRunning2.BackColor = Color.Green;
            }
            else
            {
                statusLabelRunning2.Text = "NOT RUNNING";
                statusLabelRunning2.BackColor = Color.Red;
            }
        }

        private void searchBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
