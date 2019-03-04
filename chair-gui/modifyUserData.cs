using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace study1_manager_gui
{
    public partial class modifyUserData : Form
    {
        public modifyUserData()
        {
            InitializeComponent();
        }

        private Participant participant;
        private mainForm form;

        public void setParticipant(Participant participant, mainForm form)
        {
            this.form = form;
            this.participant = participant;

            label3.Text = "Modify data for " + participant.ID;
        }

        private void modifyUserData_Load(object sender, EventArgs e)
        {

        }

        private void modifyButtonDialog_Click(object sender, EventArgs e)
        {
            participant.email = emailBox.Text;
            participant.notes = notesBox.Text;

            form.saveParticipantDetails(participant);

            this.Hide();
        }
    }
}
