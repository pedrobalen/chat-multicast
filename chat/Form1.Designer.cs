
namespace chat
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            connect_button = new Button();
            exit_button = new Button();
            send_button = new Button();
            chat_box = new RichTextBox();
            ip_field = new TextBox();
            port_field = new TextBox();
            username_field = new TextBox();
            ip_label = new Label();
            username_label = new Label();
            port_label = new Label();
            sendmessagetext_field = new TextBox();
            SuspendLayout();
            // 
            // connect_button
            // 
            connect_button.Location = new Point(632, 422);
            connect_button.Name = "connect_button";
            connect_button.Size = new Size(75, 23);
            connect_button.TabIndex = 0;
            connect_button.Text = "Connect";
            connect_button.UseVisualStyleBackColor = true;
            connect_button.Click += button1_Click;
            // 
            // exit_button
            // 
            exit_button.Location = new Point(713, 422);
            exit_button.Name = "exit_button";
            exit_button.Size = new Size(75, 23);
            exit_button.TabIndex = 1;
            exit_button.Text = "Exit";
            exit_button.UseVisualStyleBackColor = true;
            exit_button.Click += exit_button_Click;
            // 
            // send_button
            // 
            send_button.Location = new Point(713, 386);
            send_button.Name = "send_button";
            send_button.Size = new Size(75, 23);
            send_button.TabIndex = 2;
            send_button.Text = "Send";
            send_button.UseVisualStyleBackColor = true;
            send_button.Click += button2_Click;
            // 
            // chat_box
            // 
            chat_box.Location = new Point(12, 12);
            chat_box.Name = "chat_box";
            chat_box.Size = new Size(776, 368);
            chat_box.TabIndex = 3;
            chat_box.Text = "";
            chat_box.TextChanged += richTextBox1_TextChanged;
            // 
            // ip_field
            // 
            ip_field.Location = new Point(173, 422);
            ip_field.Name = "ip_field";
            ip_field.Size = new Size(100, 23);
            ip_field.TabIndex = 4;
            ip_field.TextChanged += textBox1_TextChanged;
            // 
            // port_field
            // 
            port_field.Location = new Point(333, 423);
            port_field.Name = "port_field";
            port_field.Size = new Size(100, 23);
            port_field.TabIndex = 5;
            port_field.TextChanged += port_field_TextChanged;
            // 
            // username_field
            // 
            username_field.Location = new Point(526, 423);
            username_field.Name = "username_field";
            username_field.Size = new Size(100, 23);
            username_field.TabIndex = 6;
            username_field.TextChanged += username_field_TextChanged;
            // 
            // ip_label
            // 
            ip_label.AutoSize = true;
            ip_label.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ip_label.Location = new Point(144, 418);
            ip_label.Name = "ip_label";
            ip_label.Size = new Size(23, 21);
            ip_label.TabIndex = 7;
            ip_label.Text = "IP";
            // 
            // username_label
            // 
            username_label.AutoSize = true;
            username_label.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            username_label.Location = new Point(439, 421);
            username_label.Name = "username_label";
            username_label.Size = new Size(81, 21);
            username_label.TabIndex = 8;
            username_label.Text = "Username";
            username_label.Click += label2_Click;
            // 
            // port_label
            // 
            port_label.AutoSize = true;
            port_label.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            port_label.Location = new Point(289, 421);
            port_label.Name = "port_label";
            port_label.Size = new Size(38, 21);
            port_label.TabIndex = 9;
            port_label.Text = "Port";
            port_label.Click += label3_Click;
            // 
            // sendmessagetext_field
            // 
            sendmessagetext_field.Location = new Point(12, 384);
            sendmessagetext_field.Name = "sendmessagetext_field";
            sendmessagetext_field.Size = new Size(695, 23);
            sendmessagetext_field.TabIndex = 10;
            sendmessagetext_field.KeyPress += sendmessagetext_field_KeyPress;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(sendmessagetext_field);
            Controls.Add(port_label);
            Controls.Add(username_label);
            Controls.Add(ip_label);
            Controls.Add(username_field);
            Controls.Add(port_field);
            Controls.Add(ip_field);
            Controls.Add(chat_box);
            Controls.Add(send_button);
            Controls.Add(exit_button);
            Controls.Add(connect_button);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        private void username_field_TextChanged(object sender, EventArgs e)
        {
        }

        private void port_field_TextChanged(object sender, EventArgs e)
        {
        }

        #endregion

        private Button connect_button;
        private Button exit_button;
        private Button send_button;
        private RichTextBox chat_box;
        private TextBox ip_field;
        private TextBox port_field;
        private TextBox username_field;
        private Label ip_label;
        private Label username_label;
        private Label port_label;
        private TextBox sendmessagetext_field;
    }
}
