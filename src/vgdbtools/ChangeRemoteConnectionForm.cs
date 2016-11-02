namespace Vurdalakov
{
    using System;
    using System.Windows.Forms;

    public partial class ChangeRemoteConnectionForm : Form
    {
        public ChangeRemoteConnectionForm()
        {
            InitializeComponent();
        }

        public String GetAddress()
        {
            return this.comboBoxConnection.Text.Split('@')[1];
        }

        public String GetUser()
        {
            return this.comboBoxConnection.Text.Split('@')[0];
        }

        public void AddConnections(String[] connections)
        {
            foreach (var connection in connections)
            {
                this.comboBoxConnection.Items.Add(connection);
            }
        }

        public void SetConnection(String connection)
        {
            for (var i = 0; i < this.comboBoxConnection.Items.Count; i++)
            {
                if (this.comboBoxConnection.Items[i].Equals(connection))
                {
                    this.comboBoxConnection.SelectedIndex = i;
                    return;
                }
            }

            if (this.comboBoxConnection.Items.Count > 0)
            {
                this.comboBoxConnection.SelectedIndex = 0;
            }
        }
    }
}
