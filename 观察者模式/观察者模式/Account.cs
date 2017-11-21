using System;
using System.Collections.Generic;

#region 接口,抽象观察者
//接受到消息的接口
public interface IReceive
{
    //接受到消息后要执行的方法
    void Execute(MyEventType type);
    //获取当前继承接口类的对象观察的事件
    List<MyEventType> GetMsgList();
}
#endregion

#region 手机观察者(具体观察者)
//手机类:继承于IReceive接口
public class Phone : IReceive
{
    //手机接到消息的方法
    public void Execute(MyEventType type)
    {
        if (type == MyEventType.GetMoney)
        {
            Console.WriteLine("我的谷歌亲儿子收到了一条收到钱消息");
        }
        else
        {
            Console.WriteLine("我的谷歌亲儿子提示示丢钱了");
        }

    }
    //手机的监听事件
    public List<MyEventType> GetMsgList()
    {
        List<MyEventType> msg = new List<MyEventType>();
        msg.Add(MyEventType.GetMoney);
        msg.Add(MyEventType.LostMoney);
        return msg;
    }
}

#endregion

#region 邮箱观察者
public class Email : IReceive
{

    public void Execute(MyEventType type)
    {
        Console.WriteLine("我的谷歌邮箱收到了一封通知,告诉我收到钱了");
    }

    public List<MyEventType> GetMsgList()
    {
        List<MyEventType> msg = new List<MyEventType>();
        msg.Add(MyEventType.GetMoney);
        return msg;
    }
}

#endregion


//声明事件类型的枚举
public enum MyEventType
{
    LostMoney,
    GetMoney
}

#region 被观察者
public class Account
{
    Dictionary<IReceive, List<MyEventType>> dic;

    public Account()
    {
        //集合初始化
        dic = new Dictionary<IReceive, List<MyEventType>>();
        //注册当前账户的观察者
        //将电话注册为账户的观察者
        RegisterObserver(new Phone());
        //将邮箱注册为账户的观察者
        RegisterObserver(new Email());

    }
    //注册观察者的方法
    public void RegisterObserver(IReceive observer)
    {
        dic.Add(observer, observer.GetMsgList());
    }
    //向外界发送消息的方法,这个方法的触发是账户发生变化时调用
    public void SendMessage(MyEventType type)
    {
        //遍历监听当前账户的观察者集合
        foreach (var item in dic)
        {
            //如果当前对象的监听事件列表中有账户发送过来的事件,就执行回调方法Excute();
            if (item.Value.Contains(type))
            {
                item.Key.Execute(type);
            }
        }
    }
}
#endregion

