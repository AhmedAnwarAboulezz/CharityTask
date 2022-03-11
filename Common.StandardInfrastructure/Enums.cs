using System;
using System.Collections.Generic;
using System.Reflection;
// ReSharper disable StringLiteralTypo
namespace Common.StandardInfrastructure
{
    public enum Filter_GridEnum
    {
        Contains = 0,
        StartWith = 1,
        EndWith = 2
    }

    public enum ProductTypeEnum
    {
        [EnumStringNameValue("Edible", "Edible")]
        Edible = 1,
        [EnumStringNameValue("Donated", "Donated")]
        Donated = 2
    }

    public enum GenderEnum
    {
        [EnumGuid("1160f693-bef5-e011-a485-80ee7300c611")]
        [EnumStringNameValue("Male", "ذكر")]
        Male,
        [EnumGuid("5160f693-bef5-e011-a485-80ee7300c612")]
        [EnumStringNameValue("Female", "أنثي")]
        Female,
        [EnumGuid("2260f693-bef5-e011-a485-80ee7300c693")]
        [EnumStringNameValue("Both", "كلاهما")]
        Both
    }





    public enum ActionTypeEnum
    {
        [EnumGuid("10000000-1000-1000-1000-100000000000")]
        [EnumStringNameValue("Opened", "مفتوح")]
        Open,
        [EnumGuid("20000000-2000-2000-2000-200000000000")]
        [EnumStringNameValue("Closed", "مغلق")]
        Closed,
    }

    
    // Enums For Expression generator
    public enum OperationExpressionEnum
    {
        EqualTo,
        NotEqualTo,
    }
    public enum SelectorsEnum
    {
        And = 1,
        Or = 2,
        NotNullAnd = 3,
        NotNullOr = 4
    }
    // Helper for Enum Guid
    class EnumGuid : Attribute
    {
        public Guid Guid;

        public EnumGuid(string guid)
        {
            Guid = new Guid(guid);
        }
    }
    class EnumStringNotificationValue
    {
        public string OldUrl;
        public string NewUrl;
        public EnumStringNotificationValue(string oldUrl, string newUrl)
        {
            this.OldUrl = oldUrl;
            this.NewUrl = newUrl;
        }
    }
    class EnumStringNameValue : Attribute
    {
        public string NameFl;
        public string NameSl;
        public bool CanShow;
        public bool CanShowMobile;
        public EnumStringNameValue(string nameFl, string nameSl)
        {
            this.NameFl = nameFl;
            this.NameSl = nameSl;
        }
        public EnumStringNameValue(string nameFl, string nameSl, bool canShow, bool canShowMobile)
        {
            this.NameFl = nameFl;
            this.NameSl = nameSl;
            this.CanShow = canShow;
            this.CanShowMobile = canShowMobile;
        }
    }
    public static class EnumExtensionsClass
    {
        public static Guid GetEnumGuid(this Enum e)
        {
            Type type = e.GetType();

            MemberInfo[] memInfo = type.GetMember(e.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(EnumGuid), false);
                if (attrs != null && attrs.Length > 0)
                    return ((EnumGuid)attrs[0]).Guid;
            }

            throw new ArgumentException("Enum " + e.ToString() + " has no EnumGuid defined!");
        }
        public static List<string> GetName(this Enum e, bool isMigration)
        {
            var type = e.GetType();

            var memInfo = type.GetMember(e.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(EnumStringNameValue), false);
                if (attrs != null && attrs.Length > 0)
                {
                    var attributes = (EnumStringNameValue)attrs[0];
                    var list = new List<string>
                    {
                       attributes.NameFl,
                       attributes.NameSl
                    };
                    if (attributes?.CanShow != null) list.Add(attributes?.CanShow.ToString());
                    if (attributes?.CanShowMobile != null) list.Add(attributes?.CanShow.ToString());

                    return list;
                }
            }

            throw new ArgumentException("Name " + e.ToString() + " has no Name defined!");
        }

      
        public static List<string> GetName(this Enum e)
        {
            var type = e.GetType();

            var memInfo = type.GetMember(e.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(EnumStringNameValue), false);
                if (attrs != null && attrs.Length > 0)
                {
                    var attributes = (EnumStringNameValue)attrs[0];
                    var list = new List<string>
                    {
                        Helper.ChangeProperty()==(int)Helper.ChangePropertyEnum.Ar || Helper.ChangeProperty()==(int)Helper.ChangePropertyEnum.ArEn ? attributes.NameSl : attributes.NameFl,
                         Helper.ChangeProperty()==(int)Helper.ChangePropertyEnum.En || Helper.ChangeProperty()==(int)Helper.ChangePropertyEnum.ArEn ? attributes.NameFl : attributes.NameSl
                    };
                    if (attributes?.CanShow != null) list.Add(attributes?.CanShow.ToString());
                    if (attributes?.CanShowMobile != null) list.Add(attributes?.CanShow.ToString());

                    return list;
                }
            }

            throw new ArgumentException("Name " + e.ToString() + " has no Name defined!");
        }
        public static T GetEnum<T>(this Guid guid) where T : struct, IConvertible
        {
            var enums = Enum.GetValues(typeof(T));
            T result = new T();
            foreach (var item in enums)
            {
                if (((Enum)item).GetEnumGuid() == guid) result = (T)item;
            }
            return result;
        }


    }
}