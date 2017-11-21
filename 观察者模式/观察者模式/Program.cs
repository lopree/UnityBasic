using System;
using System.Collections.Generic;

namespace 观察者模式
{
    class MainClass
    {
        public static void Main(String[] args)
        {
            //当前用户的账户
            Account account = new Account();
            //检测到收到钱
            account.SendMessage(MyEventType.GetMoney);
            Console.WriteLine("=============");
            //检测到丢失钱
            account.SendMessage(MyEventType.LostMoney);
        }
    }
}