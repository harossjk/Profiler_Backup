// Decompiled with JetBrains decompiler
// Type: UDMProfilerV3.CViewTagS`1
// Assembly: UDMProfilerV3, Version=3.18.6.21, Culture=neutral, PublicKeyToken=null
// MVID: 2C643342-5821-4FBA-8F05-6AB41E149AE6
// Assembly location: C:\Program Files (x86)\UDMTEK\UDMProfilerV3\UDMProfilerV3.exe

using System.Collections.Generic;
using System.Linq;
using UDM.Common;

namespace UDMProfilerV3
{
    public class CViewTagS<T> : Dictionary<CTag, T> where T : IViewTag, new()
    {
        private CTagS m_cSourceTagS = null;
        private List<string> m_lstUserTagSourceKey = new List<string>();
        private List<T> m_lstUserViewTag = new List<T>();

        public CViewTagS()
        {
        }

        public CViewTagS(CTagS cSourceTagS)
        {
            CreateInstance(cSourceTagS);
        }

        public void Dispose()
        {
            Clear();
            m_cSourceTagS = null;
        }

        public CTagS SourceTagS
        {
            get
            {
                return m_cSourceTagS;
            }
        }

        public List<T> UserViewTagList
        {
            get
            {
                return m_lstUserViewTag;
            }
            set
            {
                m_lstUserViewTag = value;
            }
        }

        public bool IsExistTag(string sKey)
        {
            if (m_cSourceTagS.ContainsKey(sKey) && m_cSourceTagS[sKey].Creator == "System")
                return true;
            bool flag = false;
            for (int index = 0; index < m_lstUserViewTag.Count; ++index)
            {
                if (m_lstUserViewTag[index].Key == sKey)
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }

        public new void Add(CTag cTag, T cViewTag)
        {
            if (cTag == null || (object)cViewTag == null)
                return;
            base.Add(cTag, cViewTag);
            if (!(cViewTag.Creator != "System"))
                return;
            m_lstUserViewTag.Add(cViewTag);
        }

        public void Remove(CTag cTag)
        {
            T obj = this[cTag];
            if (obj.Creator != "System" && obj.Tag != null)
                m_lstUserViewTag.Remove(obj);
            base.Remove(cTag);
        }

        public void RemoveAllUserTag()
        {
            for (int index = 0; index < m_lstUserViewTag.Count; ++index)
                base.Remove(m_lstUserViewTag[index].Tag);
            m_lstUserViewTag.Clear();
        }

        public new void Clear()
        {
            base.Clear();
            m_lstUserTagSourceKey.Clear();
            m_lstUserViewTag.Clear();
        }

        public void Apply(bool bTagIsAddable)
        {
            if (bTagIsAddable)
            {
                // 사용자 태그 동기화를 위해 소스 태그에서 사용자 태그 제거
                for (int index = 0; index < m_lstUserTagSourceKey.Count; ++index)
                    m_cSourceTagS.Remove(m_lstUserTagSourceKey[index]);

                m_lstUserTagSourceKey.Clear();
                m_lstUserViewTag.Clear();

                for (int index = 0; index < this.Count; ++index)
                {
                    T obj = this.ElementAt<KeyValuePair<CTag, T>>(index).Value;
                    obj.Apply();

                    if (obj.Creator != "System")
                    {
                        m_cSourceTagS.Add(obj.Key, obj.Tag);
                        m_lstUserTagSourceKey.Add(obj.Key);
                        m_lstUserViewTag.Add(obj);
                    }
                }
            }
            else
            {
                for (int index = 0; index < this.Count; ++index)
                    this.ElementAt<KeyValuePair<CTag, T>>(index).Value.Apply();
            }
        }

        public List<T> GetTotalViewTagList()
        {
            return Values.ToList<T>();
        }

        public T Find(CTag cTag)
        {
            return this[cTag];
        }

        private void CreateInstance(CTagS cSourceTagS)
        {
            m_cSourceTagS = cSourceTagS;

            for (int i = 0; i < cSourceTagS.Count; ++i)
            {
                KeyValuePair<string, CTag> keyValuePair = cSourceTagS.ElementAt<KeyValuePair<string, CTag>>(i);
                CTag tag = keyValuePair.Value;

                if (keyValuePair.Key != tag.Key)
                    tag.Key = keyValuePair.Key;

                T obj = new T();
                obj.Tag = tag;

                base.Add(tag, obj);

                if (obj.Creator != "System" && obj.Tag != null)
                {
                    m_lstUserViewTag.Add(obj);
                    m_lstUserTagSourceKey.Add(obj.Key);
                }
            }
        }
    }
}
