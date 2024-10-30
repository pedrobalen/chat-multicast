using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace chat
{
    public partial class Form1 : Form
    {
        private UdpCommunicator udpCommunicator;
        private List<string> messagesList = new List<string>();
        private Thread receiverThread;
        private bool isRunning = true;

        public Form1()
        {
            InitializeComponent();
            ip_field.Text = "239.1.2.3";
            port_field.Text = "3456";
        }

        private void button1_Click(object sender, EventArgs e) // Connect
        {
            try
            {
                if (string.IsNullOrEmpty(username_field.Text))
                {
                    MessageBox.Show("Please enter your nickname");
                    return;
                }

                udpCommunicator = new UdpCommunicator(ip_field.Text, int.Parse(port_field.Text));

                receiverThread = new Thread(new ThreadStart(ReceiveMessages));
                receiverThread.IsBackground = true;
                receiverThread.Start();

                MessageBox.Show("Successfully connected!");
                connect_button.Enabled = false;
                username_field.Enabled = false;
                ip_field.Enabled = false;
                port_field.Enabled = false;

                udpCommunicator.SendMessage($"{username_field.Text} joined the room");
                sendmessagetext_field.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void ReceiveMessages()
        {
            while (isRunning)
            {
                try
                {
                    string message = udpCommunicator.ReceiveMessage();
                    Invoke((MethodInvoker)delegate
                    {
                        messagesList.Add(message);
                        UpdateMessageDisplay();
                    });
                }
                catch (Exception ex)
                {
                    if (isRunning)
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            MessageBox.Show(ex.Message);
                        });
                    }
                }
            }
        }

        private void UpdateMessageDisplay()
        {
            chat_box.Clear(); 

            foreach (var message in messagesList)
            {
                string[] parts = message.Split(new[] { ": " }, 2, StringSplitOptions.None); // Divide em nome e conteúdo
                if (parts.Length == 2)
                {
                    string username = parts[0];
                    string content = parts[1];

            
                    chat_box.SelectionFont = new Font(chat_box.Font, FontStyle.Regular);
                    chat_box.AppendText($"[{DateTime.Now:HH:mm}] ");

               
                    chat_box.SelectionFont = new Font(chat_box.Font, FontStyle.Bold);
                    chat_box.AppendText(username + ": ");

               
                    chat_box.SelectionFont = new Font(chat_box.Font, FontStyle.Regular);
                    chat_box.AppendText(content + Environment.NewLine);
                }
                else
                {
                    
                    chat_box.SelectionFont = new Font(chat_box.Font, FontStyle.Regular);
                    chat_box.AppendText(message + Environment.NewLine);
                }
            }

           
            chat_box.SelectionStart = chat_box.Text.Length;
            chat_box.ScrollToCaret();
        }



        private void button2_Click(object sender, EventArgs e) // Send message
        {
            if (udpCommunicator != null)
            {
                string message = $"{username_field.Text}: {sendmessagetext_field.Text}";
                udpCommunicator.SendMessage(message);
                sendmessagetext_field.Text = "";
            }
            else
            {
                MessageBox.Show("Connect before sending messages.");
            }
        }

        private void exit_button_Click(object sender, EventArgs e) // Quit
        {
            ExitSystem();
        }

        private void ExitSystem()
        {
            if (udpCommunicator != null)
            {
                udpCommunicator.LeaveGroup($"{username_field.Text} left the room");
                udpCommunicator.Dispose();
            }
            Application.Exit();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            isRunning = false;
            udpCommunicator?.Dispose();
            base.OnFormClosing(e);
        }

        private void sendmessagetext_field_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) 
            {
                e.Handled = true; 

                if (udpCommunicator != null)
                {
                    string message = $"{username_field.Text}: {sendmessagetext_field.Text}";
                    udpCommunicator.SendMessage(message);
                    sendmessagetext_field.Text = ""; 
                }
                else
                {
                    MessageBox.Show("Connect before sending messages.");
                }
            }
        }

    }
}
