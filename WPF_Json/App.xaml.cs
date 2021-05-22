using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_Json
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            func1();
        }


        static void func1()
        {
            //创建json对象
            JObject obj = new JObject();
            obj.Add("Name", "Jack");//写法1
            obj.Add(new JProperty("Department", "Personnel Department"));//写法2
            obj.Add(new JProperty("Age", 33));
            obj.Add(new JProperty("Leader", new JObject(new JProperty("Name", "Tom"), new JProperty("Age", 44), new JProperty("Department", "Personnel Department"))));

            Console.WriteLine(obj.ToString());

            //          {
            //              "Name": "Jack",
            //"Department": "Personnel Department",
            //"Age": 33,
            //"Leader": {
            //                  "Name": "Tom",
            //  "Age": 44,
            //  "Department": "Personnel Department"
            //}
            //          }
        }

        static void func2()
        {
            //创建json数组

            JArray arr = new JArray();
            arr.Add(new JValue(1));
            arr.Add(new JValue(2));
            arr.Add(new JValue(3));
            Console.WriteLine(arr.ToString());

            //            [
            //  1,
            //  2,
            //  3
            //]

        }

        static void func3()
        {
            //解析并查询
            string json = "{\"Name\" : \"Jack\", \"Age\" : 34, \"Colleagues\" : [{\"Name\" : \"Tom\", \"Age\" : 44}, {\"Name\" : \"Abel\", \"Age\":29}]}";
            JObject obj = JObject.Parse(json);


            JToken ageToken = obj["Age"];
            Console.WriteLine(ageToken.ToString());

            //34
        }

        static void func4()
        {
            //解析并修改
            string json = "{\"Name\" : \"Jack\", \"Age\" : 34, \"Colleagues\" : [{\"Name\" : \"Tom\", \"Age\" : 44}, {\"Name\" : \"Abel\", \"Age\":29}]}";
            JObject obj = JObject.Parse(json);

            obj["Age"] = 35;

            JToken colleagues = obj["Colleagues"];
            colleagues[0]["Age"] = 45;
            obj["Colleagues"] = colleagues;//修改完后记得赋值回去

            Console.WriteLine(obj.ToString());

            //            {
            //                "Name": "Jack",
            //  "Age": 35,
            //  "Colleagues": [
            //    {
            //      "Name": "Tom",
            //      "Age": 45
            //    },
            //    {
            //      "Name": "Abel",
            //      "Age": 29
            //    }
            //  ]
            //}
        }

        static void func5()
        {
            //解析并删除
            string json = "{\"Name\" : \"Jack\", \"Age\" : 34, \"Colleagues\" : [{\"Name\" : \"Tom\", \"Age\" : 44}, {\"Name\" : \"Abel\", \"Age\":29}, {\"Name\" : \"Jully\", \"Age\" : 23}]}";
            JObject obj = JObject.Parse(json);

            obj["Colleagues"][0].Remove();

            Console.WriteLine(obj.ToString());

            //            {
            //                "Name": "Jack",
            //  "Age": 34,
            //  "Colleagues": [
            //    {
            //      "Name": "Abel",
            //      "Age": 29
            //    },
            //    {
            //      "Name": "Jully",
            //      "Age": 23
            //    }
            //  ]
            //}

            obj.Remove("Colleagues");

            Console.WriteLine(obj.ToString());

            //          {
            //              "Name": "Jack",
            //"Age": 34
            //          }


        }

        static void func6()
        {
            //解析并添加
            string json = "{\"Name\" : \"Jack\", \"Age\" : 34, \"Colleagues\" : [{\"Name\" : \"Tom\", \"Age\" : 44}, {\"Name\" : \"Abel\", \"Age\":29}, {\"Name\" : \"Jully\", \"Age\" : 23}]}";
            JObject obj = JObject.Parse(json);

            obj["Age"].Parent.AddAfterSelf(new JProperty("Department", "Personnel Department"));

            //Console.WriteLine(obj.ToString());

            //            {
            //                "Name": "Jack",
            //  "Age": 34,
            //  "Department": "Personnel Department",
            //  "Colleagues": [
            //    {
            //      "Name": "Tom",
            //      "Age": 44
            //    },
            //    {
            //      "Name": "Abel",
            //      "Age": 29
            //    },
            //    {
            //      "Name": "Jully",
            //      "Age": 23
            //    }
            //  ]
            //}

            JObject linda = new JObject(new JProperty("Name", "Linda"), new JProperty("Age", "23"));
            obj["Colleagues"].Last.AddAfterSelf(linda);

            Console.WriteLine(obj.ToString());

            //            {
            //                "Name": "Jack",
            //  "Age": 34,
            //  "Department": "Personnel Department",
            //  "Colleagues": [
            //    {
            //      "Name": "Tom",
            //      "Age": 44
            //    },
            //    {
            //      "Name": "Abel",
            //      "Age": 29
            //    },
            //    {
            //      "Name": "Jully",
            //      "Age": 23
            //    },
            //    {
            //      "Name": "Linda",
            //      "Age": "23"
            //    }
            //  ]
            //}
        }

        static void func7()
        {
            //解析并简化查询(SelectToken)
            string json = "{\"Name\" : \"Jack\", \"Age\" : 34, \"Colleagues\" : [{\"Name\" : \"Tom\", \"Age\" : 44}, {\"Name\" : \"Abel\", \"Age\":29}, {\"Name\" : \"Jully\", \"Age\" : 23}]}";
            JObject obj = JObject.Parse(json);

            //1.
            JToken name = obj.SelectToken("Name");

            Console.WriteLine(name.ToString());

            //2.
            var names = obj.SelectToken("Colleagues").Select(p => p["Name"]).ToList();
            foreach (var theName in names)
            {
                Console.WriteLine(theName.ToString());
            }

            //3.
            var age = obj.SelectToken("Colleagues[2].Age");
            Console.WriteLine(age.ToString());
        }

        static void func8()
        {
            //删除列表里a节点的值为aa的项
            string json = "[{'a' : 'aaa', 'b' : 'bbb', 'c' : 'ccc'}, {'a' : 'aa', 'b' : 'bb', 'c' : 'cc'}]";
            var obj = JArray.Parse(json);//JObject:JSON对象  JArray:JSON数组

            //Console.WriteLine(obj.ToString());

            //            [
            //  {
            //    "a": "aaa",
            //    "b": "bbb",
            //    "c": "ccc"
            //  },
            //  {
            //    "a": "aa",
            //    "b": "bb",
            //    "c": "cc"
            //  }
            //]

            IList<JToken> delList = new List<JToken>();//存储需要删除的项
            foreach (var token in obj)
            {
                if (((JObject)token)["a"].ToString() == "aa")
                {
                    delList.Add(token);
                }
            }
            //删除
            foreach (var token in delList)
            {
                obj.Remove(token);
            }
            Console.WriteLine(obj.ToString());

            //[
            //  {
            //    "a": "aaa",
            //    "b": "bbb",
            //    "c": "ccc"
            //  }
            //]

        }

        static void func9()
        {
            string json = @"
[{'Languages':['C#','Java'],'Name':'李志伟','Sex':true},
{'Languages':['C#','C++'],'Name':'Coder2','Sex':false},
{'Languages':['C#','C++','C','Java'],'Name':'Coder3', 'Sex':true}]";

            //利用JsonTextReader进行读(不常用)
            JsonTextReader jsonTextReader = new JsonTextReader(new StringReader(json));
            while (jsonTextReader.Read())
            {
                Console.WriteLine(jsonTextReader.Value + "--" + jsonTextReader.TokenType + "--" + jsonTextReader.ValueType);
            }

            //--StartArray--
            //--StartObject--
            //Languages--PropertyName--System.String
            //--StartArray--
            //C#--String--System.String
            //Java--String--System.String
            //--EndArray--
            //Name--PropertyName--System.String
            //李志伟--String--System.String
            //Sex--PropertyName--System.String
            //True--Boolean--System.Boolean
            //--EndObject--
            //--StartObject--
            //Languages--PropertyName--System.String
            //--StartArray--
            //C#--String--System.String
            //C++--String--System.String
            //--EndArray--
            //Name--PropertyName--System.String
            //Coder2--String--System.String
            //Sex--PropertyName--System.String
            //False--Boolean--System.Boolean
            //--EndObject--
            //--StartObject--
            //Languages--PropertyName--System.String
            //--StartArray--
            //C#--String--System.String
            //C++--String--System.String
            //C--String--System.String
            //Java--String--System.String
            //--EndArray--
            //Name--PropertyName--System.String
            //Coder3--String--System.String
            //Sex--PropertyName--System.String
            //True--Boolean--System.Boolean
            //--EndObject--
            //--EndArray--
        }

        static void func10()
        {
            string json = @"
[{'Languages':['C#','Java'],'Name':'李志伟','Sex':true},
{'Languages':['C#','C++'],'Name':'Coder2','Sex':false},
{'Languages':['C#','C++','C','Java'],'Name':'Coder3', 'Sex':true}]";

            //利用JArray JObject JToken进行读写(常用)
            //先前已知json结构
            JArray jarray = (JArray)JsonConvert.DeserializeObject(json);
            foreach (JToken jtoken in jarray)
            {
                JObject jobject = (JObject)jtoken;
                JArray temp = (JArray)jobject["Languages"];
                foreach (JToken jtoken_sub in temp)
                {
                    Console.WriteLine(jtoken_sub + " ");
                }
                Console.WriteLine("\t" + jobject["Name"] + "\t" + jobject["Sex"]);
            }

            //            C#
            //Java
            //        李志伟 True
            //C#
            //C++
            //        Coder2  False
            //C#
            //C++
            //C
            //Java
            //        Coder3 True

        }

        static void func11()
        {
            //时间字符串的处理

            DateTime time = DateTime.Now;
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
            timeConverter.DateTimeFormat = "北京时间：yyyy-MM-dd HH:mm:ss";

            //序列化时间
            string json = JsonConvert.SerializeObject(time, timeConverter);
            Console.WriteLine(json);
            //反序列化时间
            DateTime jsontime = JsonConvert.DeserializeObject<DateTime>(json, timeConverter);
            Console.WriteLine(jsontime);

            //"北京时间：2020-11-24 10:03:56"
            //2020 / 11 / 24 10:03:56
        }



    }
}
