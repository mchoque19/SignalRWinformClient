using Microsoft.AspNetCore.SignalR.Client;

namespace SignalRWinformClient
{
    public partial class Form1 : Form
    {
        HubConnection hubConnection;
        public Form1()
        {
            InitializeComponent();
            hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7075/ChatHub")
                .Build();

            hubConnection.Closed += HubConnection_Closed;
        }

        private async Task HubConnection_Closed(Exception? arg)
        {
            await Task.Delay(new Random().Next(0,5) * 1000);
            await hubConnection.StartAsync();

        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                var newMessage = $"{user}: {message}";
                //listBox1.Items.Add(newMessage);

                //MessageBox.Show(newMessage);
            });
            try
            {
                await hubConnection.StartAsync();
                listBox1.Items.Add("Connection started");

            }
            catch (Exception ex)
            {

               listBox1.Items.Add(ex.Message);

            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                await hubConnection.InvokeAsync("SendMessage", textBox1.Text, textBox2.Text);

            }
            catch (Exception ex)
            {
                listBox1.Items.Add(ex.Message);
                
            }
        }
    }
}