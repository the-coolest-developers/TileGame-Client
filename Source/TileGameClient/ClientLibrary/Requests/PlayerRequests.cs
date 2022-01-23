using ClientLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClientLibrary.Requests
{
    public class PlayerRequests
    {
        public BaseRequestSender _baseSender;
        
        public PlayerRequests()
        {
            _baseSender = new BaseRequestSender(TimeSpan.FromSeconds(30));//Сделать по-другому
        }

        public static async Task Register(RegisterNicknameModel model) 
        {
            //_baseSender.Post()
        }
    }
}
