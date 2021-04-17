using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//本例使用自定义的 [ExportCard... 而不是[Export...
//好处是
//1.可以定义自定义属性，比如这里使用自定义属性区分继承接口的不同派生类类型
//2.使用特性时不需要再指定接口类型，因为特性内部已经定义好了

namespace BankInterface
{
    //ICard和IMetadata接口组合是为了使用 Lazy<ICard, IMetadata>

    public interface ICard
    {
        /// <summary>
        /// 账户金额
        /// </summary>
        double Money { get; set; }

        /// <summary>
        /// 获取账户信息
        /// </summary>
        /// <returns></returns>
        string GetCountInfo();

        /// <summary>
        /// 存钱
        /// </summary>
        /// <param name="money"></param>
        void SaveMoney(double money);

        /// <summary>
        /// 取钱
        /// </summary>
        /// <param name="money"></param>
        void CheckOutMoney(double money);
    }

    public interface IMetadata
    {
        string CardType { get; }
    }

    //创建基于System.ComponentModel.Composition的自定义特性
    //1.[MetadataAttribute]
    //2.派生自ExportAttribute，而非Attribute
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ExportCardAttribute : ExportAttribute
    {
        public ExportCardAttribute()
            : base(typeof(ICard))
        { }

        //用来区别每个派生自ICard的派生类类型(注意不是类对象)
        public string CardType { get; set; }
    }
}
