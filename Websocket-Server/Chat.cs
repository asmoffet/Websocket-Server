using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Websocket_Server
{
    // Task I:  Modify this websocket behavior so that it remembers all messages.
    //          Whenever there is a new client connected, send all stored messages to the client
    //          so the client knows the history of the conversation.
    // Task II: Add timestamp (just the hour and minute) to the messages
    //          (see http://stackoverflow.com/questions/21219797/how-to-get-correct-timestamp-in-c-sharp
    //           for an example on how to get a timestamp).
    class Chat : WebSocketBehavior
    {
        public static List<string> history = new List<string>();
        public static int i = 0;
        protected override void OnOpen()
        {

            foreach (string h in history)
            {
                Send(h);
            }
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            // Retrieve message from client
            string msg = e.Data + " " + DateTime.Now.ToString();
            history.Add(e.Data + " " +  DateTime.Now.ToString());
            i++;

            // Broadcast message to all clients
            Sessions.Broadcast(msg);
        }
    }
}
