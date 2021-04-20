using System;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace AntMuzyka
{
    class Program
    {
        private static TelegramBotClient client;
         

        static void Main(string[] args)
        {
            client = new TelegramBotClient(Config.Token);
            client.StartReceiving();
            
            Console.WriteLine("[Log]: Bot started");
            client.OnMessage += OnMessageHandler;


            Console.ReadLine();
            client.StopReceiving();
        }

        private static async void OnMessageHandler(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            string[] bList = { "сыл", "пар", "ссыл", "посилання", "скинь", "лекци", "дай", "плз", "пожалуйста", "ssylka", "sylka", "lek", "лекц", "par", "lec", "kint", "кинь", "отпра", "знает", "где", "что", "когда", "gde", "czto", "chto", "kogda", "сколько", "skolko", "daj", "prosz", "rzu", "otpra", "kto", "znaje", "skol", "сколь", "помоги", "pomogi", "help", "хелп", "как", "kak", "ssy", "ссы", "pz", "пж", "сси", "ssi", "дaй" };
            bool deleted = false;
            const int targetID = 519517876;
            //519517876

            if(message.Text == "ping") await client.SendTextMessageAsync(message.Chat.Id, "pong");

            if (message.Text != null)
            {
                Console.WriteLine($"New message from {message.From.FirstName}, message: {message.Text}");
                string str = message.Text.ToLower();

                if (message.From.Id == targetID)
                {
                    foreach (string s in bList)
                    {
                        if (str.Contains(s) && !deleted)
                        {
                            deleted = true;
                            await client.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                            //await client.SendTextMessageAsync(message.Chat.Id, $"Пошёл ты нахуй {message.From.FirstName}, всех заебал блять");
                        }
                    }
                }
            }
            if (message.Voice != null || message.VideoNote != null)
            {
                if (message.From.Id == targetID) await client.DeleteMessageAsync(message.Chat.Id, message.MessageId);
            }
        }
    }
}
