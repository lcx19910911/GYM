
using System;
using System.Web;
using System.Web.Caching;
using System.Collections;
using System.Linq.Expressions;
using System.Text;
using System.IO;
using GYM.Core.Extensions;

namespace GYM.Core.Web
{
	/// <summary>
	/// CacheHelper Web��������� ���,�Ƴ�,��ȡ����
	/// </summary>
    public partial class CacheHelper
    {
        /// <summary>
        /// ͨ���������ȡ��Ӧ������
        /// </summary>
        /// <typeparam name="T">����</typeparam>
        /// <param name="key">�����</param>
        /// <returns></returns>
        public static T Get<T>(string key)
        {
            return (T)HttpRuntime.Cache[key];
        }

        /// <summary>
        /// ����ȡ�û������ݣ�����з����棬������ȡ�ú��ֵ��û���򷵻�Ĭ��ֵ��
        /// </summary>
        /// <typeparam name="T">����</typeparam>
        /// <param name="key">�����</param>
        /// <param name="outValue">������Ӧ���͵�ֵ</param>
        /// <returns></returns>
        public static bool TryGetValue<T>(string key, out T outValue)
        {
            object obj = HttpRuntime.Cache[key];
            if (obj != null)
            {
                try
                {
                    outValue = (T)obj;
                    return true;
                }
                catch
                {
                    outValue = default(T);
                    return false;
                }
            }
            else
            {
                outValue = default(T);
                return false;
            }
        }

        /// <summary>
        /// �ж��Ƿ��д�key��ֵ
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Contains(string key)
        {
            if (HttpRuntime.Cache[key] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        /// <summary>
        /// ��������ӻ���(���������Ѿ����ڣ���˷���������Ч)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="lNumofMilliSeconds"></param>
        public static void Set<T>(string key, T value, long lNumofMilliSeconds)
        {
            HttpRuntime.Cache.Add(key, value, null, DateTime.Now.AddMilliseconds(lNumofMilliSeconds), TimeSpan.Zero, CacheItemPriority.NotRemovable, null);
        }

        /// <summary>
        /// ��������ӻ���(���������Ѿ����ڣ���˷���������Ч)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="tspan"></param>
        public static void Set<T>(string key, T value, TimeSpan tspan)
        {
            HttpRuntime.Cache.Add(key, value, null, DateTime.Now.Add(tspan), TimeSpan.Zero, CacheItemPriority.NotRemovable, null);
        }


        /// <summary>
        /// ��ӻ��棬�������ʱ������
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="lNumofMilliSeconds"></param>
        public static void Insert<T>(string key, T value, long lNumofMilliSeconds)
        {
            HttpRuntime.Cache.Insert(key, value, null, DateTime.Now.AddMilliseconds(lNumofMilliSeconds), TimeSpan.Zero, CacheItemPriority.NotRemovable, null);
        }

        /// <summary>
        /// ��ӻ��棬�������ʱ������
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="tspan"></param>
        public static void Insert<T>(string key, T value, TimeSpan tspan)
        {
            HttpRuntime.Cache.Insert(key, value, null, DateTime.Now.Add(tspan), TimeSpan.Zero, CacheItemPriority.NotRemovable, null);
        }

        /// <summary>
        /// ������л���
        /// </summary>
        public static void Clear()
        {
            foreach (DictionaryEntry elem in HttpRuntime.Cache)
            {
                HttpRuntime.Cache.Remove(elem.Key.ToString());
            }
        }

        /// <summary>
        /// �Ƴ���عؼ��ֵĻ���
        /// </summary>
        /// <param name="keyword">����ؼ���</param>
        public static void Remove(string keyword)
        {
            foreach (DictionaryEntry elem in HttpRuntime.Cache)
            {
                if (elem.Key.ToString().Contains(keyword))
                {
                    HttpRuntime.Cache.Remove(elem.Key.ToString());
                }
            }
        }

        /// <summary>
        /// ���ĳ���
        /// </summary>
        /// <param name="key">�����</param>
        public static void RemoveAt(string key)
        {
            if (HttpRuntime.Cache[key] != null)
            {
                HttpRuntime.Cache.Remove(key);
            }
        }

        /// <summary>
        /// ��ȡ��������ʱ��
        /// </summary>
        /// <param name="CacheTimeOption">�����ʱ�䳤��</param>
        /// <param name="CacheExpirationOption"></param>
        /// <returns></returns>
        public static DateTime GetAbsoluteExpirationTime(CacheTimeOption CacheTimeOption, CacheExpirationOption CacheExpirationOption)
        {
            if (CacheExpirationOption == CacheExpirationOption.SlidingExpiration
                || CacheTimeOption == CacheTimeOption.NotRemovable)
            {
                return Cache.NoAbsoluteExpiration;
            }
            if (CacheTimeOption == CacheTimeOption.TenSecond)
            {
                return DateTime.Now.AddSeconds((int)CacheTimeOption);
            }
            else
            return DateTime.Now.AddMinutes((int)CacheTimeOption);
        }

        /// <summary>
        /// ��ȡ��������ʱ��
        /// </summary>
        /// <param name="CacheTimeOption">�����ʱ�䳤��</param>
        /// <param name="CacheExpirationOption"></param>
        /// <returns></returns>
        public static DateTime GetAbsoluteExpirationTimeSecond(CacheTimeOption CacheTimeOption, CacheExpirationOption CacheExpirationOption)
        {
            if (CacheExpirationOption == CacheExpirationOption.SlidingExpiration
                || CacheTimeOption == CacheTimeOption.NotRemovable)
            {
                return Cache.NoAbsoluteExpiration;
            }

            return DateTime.Now.AddSeconds((int)CacheTimeOption);
        }

        /// <summary>
        /// ��ȡ����ʱ�����ʱ��
        /// </summary>
        /// <param name="CacheTimeOption">�����ʱ�䳤��</param>
        /// <param name="CacheExpirationOption"></param>
        /// <returns></returns>
        private static TimeSpan GetSlidingExpirationTime(CacheTimeOption CacheTimeOption, CacheExpirationOption CacheExpirationOption)
        {
            if (CacheExpirationOption == CacheExpirationOption.AbsoluteExpiration
                || CacheTimeOption == CacheTimeOption.NotRemovable)
            {
                return Cache.NoSlidingExpiration;
            }
            if (CacheTimeOption == CacheTimeOption.TenSecond)
            {
                return TimeSpan.FromSeconds((int)CacheTimeOption);
            }
            else
                return TimeSpan.FromMinutes((int)CacheTimeOption);
        }


        #region ��CacheTimeOption��Set����
        // ��������
        /// <summary>
        /// ��ӻ���(���������Ѿ����ڣ���˷���������Ч)
        /// </summary>
        /// <typeparam name="T">����</typeparam>
        /// <param name="key">�����</param>
        /// <param name="value">ֵ</param>
        /// <param name="CacheTimeOption">�����ʱ�䳤��</param>
        /// <param name="CacheExpirationOption"></param>
        /// <param name="dependencies">����������</param>
        /// <param name="cacheItemPriority">���ȼ�</param>
        /// <param name="callback">�ص�����</param>
        public static void Set<T>(string key, T value, CacheTimeOption CacheTimeOption, CacheExpirationOption CacheExpirationOption,
            CacheDependency dependencies, CacheItemPriority cacheItemPriority, CacheItemRemovedCallback callback)
        {
            if (!value.Equals(default(T)) && CacheTimeOption != CacheTimeOption.None)
            {
                DateTime absoluteExpiration = GetAbsoluteExpirationTime(CacheTimeOption, CacheExpirationOption);
                TimeSpan slidingExpiration = GetSlidingExpirationTime(CacheTimeOption, CacheExpirationOption);
                HttpRuntime.Cache.Add(key, value, dependencies, absoluteExpiration, slidingExpiration, cacheItemPriority, callback);
            }
        }

        //����Set<T>Ϊ���ط���
        /// <summary>
        /// ��ӻ���(���������Ѿ����ڣ���˷���������Ч)
        /// </summary>
        /// <typeparam name="T">����</typeparam>
        /// <param name="key">�����</param>
        /// <param name="value">ֵ</param>
        /// <param name="CacheTimeOption">�����ʱ�䳤��</param>
        /// <param name="CacheExpirationOption"></param>
        /// <param name="dependencies">����������</param>
        /// <param name="cacheItemPriority">���ȼ�</param>
        public static void Set<T>(string key, T value, CacheTimeOption CacheTimeOption, CacheExpirationOption CacheExpirationOption, 
            CacheDependency dependencies, CacheItemPriority cacheItemPriority)
        {
            Set(key, value, CacheTimeOption, CacheExpirationOption, dependencies, cacheItemPriority, null);
        }

        /// <summary>
        /// ��ӻ���(���������Ѿ����ڣ���˷���������Ч)
        /// </summary>
        /// <typeparam name="T">����</typeparam>
        /// <param name="key">�����</param>
        /// <param name="value">ֵ</param>
        /// <param name="CacheTimeOption">����ʱ��</param>
        /// <param name="CacheExpirationOption">�������ʱ����𣨾���/���ԣ�</param>
        /// <param name="dependencies">����������</param>
        public static void Set<T>(string key, T value, CacheTimeOption CacheTimeOption, CacheExpirationOption CacheExpirationOption, 
            CacheDependency dependencies)
        {
            Set(key, value, CacheTimeOption, CacheExpirationOption, dependencies, CacheItemPriority.NotRemovable, null);
        }

        /// <summary>
        /// ��ӻ���(���������Ѿ����ڣ���˷���������Ч)
        /// </summary>
        /// <typeparam name="T">����</typeparam>
        /// <param name="key">�����</param>
        /// <param name="value">ֵ</param>
        /// <param name="CacheTimeOption">����ʱ��</param>
        /// <param name="CacheExpirationOption">�������ʱ����𣨾���/���ԣ�</param>
        public static void Set<T>(string key, T value, CacheTimeOption CacheTimeOption, CacheExpirationOption CacheExpirationOption)
        {
            Set(key, value, CacheTimeOption, CacheExpirationOption, null, CacheItemPriority.NotRemovable, null);
        }

        /// <summary>
        /// ��ӻ���(���������Ѿ����ڣ���˷���������Ч)
        /// </summary>
        /// <typeparam name="T">����</typeparam>
        /// <param name="key">�����</param>
        /// <param name="value">ֵ</param>
        /// <param name="callback">�ص�����</param>
        /// <param name="CacheTimeOption">����ʱ��</param>
        public static void Set<T>(string key, T value, CacheTimeOption CacheTimeOption, CacheItemRemovedCallback callback)
        {
            Set(key, value, CacheTimeOption, CacheExpirationOption.AbsoluteExpiration, null, CacheItemPriority.NotRemovable, callback);
        }

        /// <summary>
        /// ��ӻ���(���������Ѿ����ڣ���˷���������Ч)
        /// </summary>
        /// <typeparam name="T">����</typeparam>
        /// <param name="key">�����</param>
        /// <param name="value">ֵ</param>
        /// <param name="CacheTimeOption">����ʱ��</param>
        public static void Set<T>(string key, T value, CacheTimeOption CacheTimeOption)
        {
            Set(key, value, CacheTimeOption, CacheExpirationOption.AbsoluteExpiration, null, CacheItemPriority.NotRemovable, null);
        }

        /// <summary>
        /// ��ӻ���(���������Ѿ����ڣ���˷���������Ч)
        /// </summary>
        /// <typeparam name="T">����</typeparam>
        /// <param name="key">�����</param>
        /// <param name="value">ֵ</param>
        public static void Set<T>(string key, T value)
        {
            Set(key, value, CacheTimeOption.Normal, CacheExpirationOption.AbsoluteExpiration, null, CacheItemPriority.NotRemovable, null);
        }
        #endregion

        #region 2010-04-26�������¼����淽��
        /// <summary>
        /// ��������Դ����ί��
        /// </summary>
        /// <returns></returns>
        public delegate T RefreshCacheDataHandler<T>();
        /// <summary>
        /// ��ref int�Ļ�������Դ����ί�У�һ�����ڷ�ҳ����
        /// </summary>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public delegate T RefreshCacheDataWithRefParamHandler<T>(ref int recordCount);
        /// <summary>
        /// ��ref int�Ļ�������Դ����ί�У�һ�����ڷ�ҳ����
        /// </summary>
        /// <param name="recordCount"></param>
        /// <param name="recordCount2"></param>
        /// <returns></returns>
        public delegate T RefreshCacheDataWithRefParamHandler2<T>(ref int recordCount, ref int recordCount2);
        /// <summary>
        /// ��out int�Ļ�������Դ����ί�У�һ�����ڷ�ҳ����
        /// </summary>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public delegate T RefreshCacheDataWithOutParamHandler<T>(out int recordCount);
        ///// <summary>
        ///// ��ȡָ��ί�еĻ������ݣ�����û��治���ڣ����Զ���������Դί�����ɻ���
        ///// </summary>
        ///// <typeparam name="T">�������ݵ�����</typeparam>
        ///// <param name="cacheTime">����ʱ��</param>
        ///// <param name="callback">����Դί��</param>
        ///// <returns></returns>
        //public static T Get<T>(CacheTimeOption cacheTime, Expression<RefreshCacheDataHandler> callback) where T : class
        //{
        //    MethodCallExpression exp = callback.Body as MethodCallExpression;
        //    StringBuilder sbKey = new StringBuilder();
        //    sbKey.Append("AUTO_CACHE_KEY___");
        //    sbKey.Append(exp.Method.DeclaringType.FullName);
        //    sbKey.Append("_");
        //    sbKey.Append(exp.Method.Name);
        //    for (int i = 0; i < exp.Arguments.Count; i++)
        //    {
        //        sbKey.Append("_");
        //        if (exp.Arguments[i] is ConstantExpression)
        //        {
        //            sbKey.Append((exp.Arguments[i] as ConstantExpression).Value);
        //        }
        //        if (exp.Arguments[i] is MemberExpression)
        //        {
        //            sbKey.Append((exp.Arguments[i] as MemberExpression).Member);      //��������������ֵȡ�������⣬��Ҫ�������㣬��ʱ��ǳ����
        //        }
        //    }
        //    string key = "";
        //    if (Contains(key))
        //    {
        //        return Get<T>(key);
        //    }
        //    T content = callback.Compile()() as T;
        //    Set<T>(key, content, cacheTime);
        //    return content;
        //}


        /// <summary>
        /// ��ȡָ��key�������ݣ�����û��治���ڣ����Զ���������Դί�����ɻ���
        /// </summary>
        /// <typeparam name="T">�������ݵ�����</typeparam>
        /// <param name="key">����key</param>
        /// <param name="cacheTime">����ʱ��</param>
        /// <param name="callback">����Դί��</param>
        /// <param name="removedCallback">����ʧЧʱ�Ļص�����</param>
        /// <returns></returns>
        public static T Get<T>(string key, CacheTimeOption cacheTime, RefreshCacheDataHandler<T> callback, 
            CacheItemRemovedCallback removedCallback) where T : class
        {
            // ������ʱ��ֱ�ӷ���
            if (cacheTime == CacheTimeOption.None)
                return callback();

            if (Contains(key))
            {
                return Get<T>(key);
            }
            T content = callback();
            Set(key, content, cacheTime, removedCallback);
            return content;
        }

        /// <summary>
        /// ��ȡָ��key�������ݣ�����û��治���ڣ����Զ���������Դί�����ɻ���
        /// </summary>
        /// <typeparam name="T">�������ݵ�����</typeparam>
        /// <param name="key">����key</param>
        /// <param name="cacheTime">����ʱ��</param>
        /// <param name="callback">����Դί��</param>
        /// <returns></returns>
        public static T Get<T>(string key, CacheTimeOption cacheTime, RefreshCacheDataHandler<T> callback)
        {
            // ������ʱ��ֱ�ӷ���
            if (cacheTime == CacheTimeOption.None)
                return callback();

            if (Contains(key))
            {
                return Get<T>(key);
            }
            T content = callback();
            Set(key, content, cacheTime);
            return content;
        }
        /// <summary>
        /// ��ȡָ��key�������ݣ�����û��治���ڣ����Զ���������Դί�����ɻ���
        /// </summary>
        /// <typeparam name="T">�������ݵ�����</typeparam>
        /// <param name="key">����key</param>
        /// <param name="recordCountKey">��¼�����Ļ���key</param>
        /// <param name="recordCount">��¼����(ref)</param>
        /// <param name="cacheTime">����ʱ��</param>
        /// <param name="callback">����Դί��</param>
        /// <returns></returns>
        public static T Get<T>(string key, string recordCountKey, ref int recordCount, CacheTimeOption cacheTime, 
            RefreshCacheDataWithRefParamHandler<T> callback) where T : class
        {
            // ������ʱ��ֱ�ӷ���
            if (cacheTime == CacheTimeOption.None)
                return callback(ref recordCount);

            
            if (Contains(recordCountKey))
            {
                recordCount = Get<int>(recordCountKey);
            }
            if (Contains(key) && recordCount != 0)
            {
                return Get<T>(key);
            }
            T content = callback(ref recordCount);
            Set(key, content, cacheTime);
            Set(recordCountKey, recordCount, cacheTime);
            return content;
        }

        /// <summary>
        /// ��ȡָ��key�������ݣ�����û��治���ڣ����Զ���������Դί�����ɻ���
        /// </summary>
        /// <typeparam name="T">�������ݵ�����</typeparam>
        /// <param name="key">����key</param>
        /// <param name="recordCountKey">��¼�����Ļ���key</param>
        /// <param name="recordCountKey2">��¼�����Ļ���key2</param>
        /// <param name="recordCount">��¼����(ref)</param>
        /// <param name="recordCount2">��¼����2(ref)</param>
        /// <param name="cacheTime">����ʱ��</param>
        /// <param name="callback">����Դί��</param>
        /// <returns></returns>
        public static T Get<T>(string key, string recordCountKey, string recordCountKey2, ref int recordCount, ref int recordCount2, CacheTimeOption cacheTime,
            RefreshCacheDataWithRefParamHandler2<T> callback) where T : class
        {
            // ������ʱ��ֱ�ӷ���
            if (cacheTime == CacheTimeOption.None)
                return callback(ref recordCount, ref recordCount2);


            if (Contains(recordCountKey))
            {
                recordCount = Get<int>(recordCountKey);
            }
            if (Contains(recordCountKey2))
            {
                recordCount2 = Get<int>(recordCountKey2);
            }
            if (Contains(key) && recordCount != 0 && recordCount2 != 0)
            {
                return Get<T>(key);
            }
            T content = callback(ref recordCount, ref recordCount2);
            Set(key, content, cacheTime);
            Set(recordCountKey, recordCount, cacheTime);
            Set(recordCountKey2, recordCount2, cacheTime);
            return content;
        }

        /// <summary>
        /// ��ȡָ��key�������ݣ�����û��治���ڣ����Զ���������Դί�����ɻ���
        /// </summary>
        /// <typeparam name="T">�������ݵ�����</typeparam>
        /// <param name="key">����key</param>
        /// <param name="recordCountKey">��¼�����Ļ���key</param>
        /// <param name="recordCount">��¼����(out)</param>
        /// <param name="cacheTime">����ʱ��</param>
        /// <param name="callback">����Դί��</param>
        /// <returns></returns>
        public static T Get<T>(string key, string recordCountKey, out int recordCount, CacheTimeOption cacheTime, 
            RefreshCacheDataWithOutParamHandler<T> callback) where T : class
        {
            // ������ʱ��ֱ�ӷ���
            if (cacheTime == CacheTimeOption.None)
                return callback(out recordCount);

            recordCount = 0;
            if (Contains(recordCountKey))
            {
                recordCount = Get<int>(recordCountKey);
            }
            if (Contains(key) && recordCount != 0)
            {
                return Get<T>(key);
            }
            T content = callback(out recordCount);
            Set(key, content, cacheTime);
            Set(recordCountKey, recordCount, cacheTime);
            return content;
        }
        /// <summary>
        /// ��ȡָ��key�������ݣ�����û��治���ڣ����Զ���������Դί�����ɻ���(�̰߳�ȫ���������һ�����ܿ���)
        /// </summary>
        /// <typeparam name="T">�������ݵ�����</typeparam>
        /// <param name="key">����key</param>
        /// <param name="cacheTime">����ʱ��</param>
        /// <param name="callback">����Դί��</param>
        /// <returns></returns>
        public static T GetWithLock<T>(string key, CacheTimeOption cacheTime, RefreshCacheDataHandler<T> callback) where T : class
        {
            // ������ʱ��ֱ�ӷ���
            if (cacheTime == CacheTimeOption.None)
                return callback();
            
            if (Contains(key))
            {
                return Get<T>(key);
            }
            lock (string.Intern(key))
            {
                if (Contains(key))
                {
                    return Get<T>(key);
                }
                else
                {
                    T content = callback();
                    Set(key, content, cacheTime);
                    return content;
                }
            }
        }
        /// <summary>
        /// ��ȡָ��key�������ݣ�����û��治���ڣ����Զ���������Դί�����ɻ���(�̰߳�ȫ���������һ�����ܿ���)
        /// </summary>
        /// <typeparam name="T">�������ݵ�����</typeparam>
        /// <param name="key">����key</param>
        /// <param name="recordCountKey">��¼�����Ļ���key</param>
        /// <param name="recordCount">��¼����(ref)</param>
        /// <param name="cacheTime">����ʱ��</param>
        /// <param name="callback">����Դί��</param>
        /// <returns></returns>
        public static T GetWithLock<T>(string key, string recordCountKey, ref int recordCount, CacheTimeOption cacheTime, 
            RefreshCacheDataWithRefParamHandler<T> callback) where T : class
        {
            // ������ʱ��ֱ�ӷ���
            if (cacheTime == CacheTimeOption.None)
                return callback(ref recordCount);

            if (Contains(recordCountKey))
            {
                recordCount = Get<int>(recordCountKey);
            }
            if (Contains(key))
            {
                return Get<T>(key);
            }
            lock (string.Intern(key))
            {
                if (Contains(recordCountKey))
                {
                    recordCount = Get<int>(recordCountKey);
                }
                if (Contains(key))
                {
                    return Get<T>(key);
                }
                else
                {
                    T content = callback(ref recordCount);
                    Set(key, content, cacheTime);
                    Set(recordCountKey, recordCount, cacheTime);
                    return content;
                }
            }
        }
        /// <summary>
        /// ��ȡָ��key�������ݣ�����û��治���ڣ����Զ���������Դί�����ɻ���(�̰߳�ȫ���������һ�����ܿ���)
        /// </summary>
        /// <typeparam name="T">�������ݵ�����</typeparam>
        /// <param name="key">����key</param>
        /// <param name="recordCountKey">��¼�����Ļ���key</param>
        /// <param name="recordCount">��¼����(out)</param>
        /// <param name="cacheTime">����ʱ��</param>
        /// <param name="callback">����Դί��</param>
        /// <returns></returns>
        public static T GetWithLock<T>(string key, string recordCountKey, out int recordCount, CacheTimeOption cacheTime, 
            RefreshCacheDataWithOutParamHandler<T> callback) where T : class
        {
            // ������ʱ��ֱ�ӷ���
            if (cacheTime == CacheTimeOption.None)
                return callback(out recordCount);

            recordCount = 0;
            if (Contains(recordCountKey))
            {
                recordCount = Get<int>(recordCountKey);
            }
            if (Contains(key))
            {
                return Get<T>(key);
            }
            lock (string.Intern(key))
            {
                if (Contains(recordCountKey))
                {
                    recordCount = Get<int>(recordCountKey);
                }
                if (Contains(key))
                {
                    return Get<T>(key);
                }
                else
                {
                    T content = callback(out recordCount);
                    Set(key, content, cacheTime);
                    Set(recordCountKey, recordCount, cacheTime);
                    return content;
                }
            }
        }
        #endregion

        /// <summary>
        /// ���ļ�����������ȡ��Ŀǰ���������ļ���
        /// </summary>
        /// <typeparam name="T">����</typeparam>
        /// <param name="key">�����</param>
        /// <param name="fileName">�ļ�ȫ��</param>
        /// <param name="cacheSetter">���ɷ���ֵ�ķ���</param>
        /// <returns></returns>
        public static T GetByFileDependency<T>(string key, string fileName, Func<T> cacheSetter)
        {
            if (CacheHelper.Contains(key))
                return CacheHelper.Get<T>(key);

            //�����Ҫ�ļ��������棬�贴����������
            if (fileName.IsNullOrEmpty() || !File.Exists(fileName))
                throw new ArgumentException("��ȡ�ļ������������ָ����ȷ���ļ�����·��,���Ҹ��ļ��Ѵ���" + fileName);

            var local = cacheSetter();
            var dep = new CacheDependency(fileName);
            Set(key, local, CacheTimeOption.NotRemovable, CacheExpirationOption.AbsoluteExpiration, dep);
            return local;
        }


        /// <summary>
        /// ����Key
        /// </summary>
        /// <param name="prefixKey"></param>
        /// <param name="arg0"></param>
        /// <returns></returns>
        public static string RenderKey(string prefixKey, string arg0)
        {
            return string.Format("{0}.{1}", prefixKey, arg0);
        }

        /// <summary>
        /// ����Key
        /// </summary>
        /// <param name="prefixKey"></param>
        /// <param name="arg0"></param>
        /// <param name="arg1"></param>
        /// <returns></returns>
        public static string RenderKey(string prefixKey, string arg0, string arg1)
        {
            return string.Format("{0}.{1}.{2}", prefixKey, arg0, arg1);
        }

        /// <summary>
        /// ����Key
        /// </summary>
        /// <param name="prefixKey"></param>
        /// <param name="arg0"></param>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <returns></returns>
        public static string RenderKey(string prefixKey, string arg0, string arg1, string arg2)
        {
            return string.Format("{0}.{1}.{2}.{3}", prefixKey, arg0, arg1, arg2);
        }

        /// <summary>
        /// ����Key
        /// </summary>
        /// <param name="prefixKey"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string RenderKey(string prefixKey, params string[] args)
        {
            var sb = new StringBuilder(prefixKey);
            for (var i = 0; i < args.Length; i++)
            {
                sb.AppendFormat(".{0}", args[i]);
            }
            return sb.ToString();
        }
    }

    /// <summary>
    /// ���ڷ�ʽ
    /// </summary>
    public enum CacheExpirationOption
    {
        /// <summary>
        /// ���Թ���
        /// </summary>
        AbsoluteExpiration = 0,
        /// <summary>
        /// ���Թ���
        /// </summary>
        SlidingExpiration = 1
    }

    /// <summary>
    /// ���ù���ʱ��
    /// </summary>
    public enum CacheTimeOption
    {
        /// <summary>
        /// ������
        /// </summary>
        None = 0,

        TenSecond=20,

        /// <summary>
        /// ��ʱ�� 3����
        /// </summary>
        Short = 3,
        /// <summary>
        /// һ����������ʱ�� 10����
        /// </summary>
        Normal = 10,
        /// <summary>
        /// ��ʱ�� 30����
        /// </summary>
        Long = 30,

        /// <summary>
        /// ��������
        /// </summary>
        NotRemovable,

        /// <summary>
        /// һ����
        /// </summary>
        OneMinute = 1,
        /// <summary>
        /// ������
        /// </summary>
        SixMinutes = 6,
        /// <summary>
        /// 10����
        /// </summary>
        TenMinutes = 10,
        /// <summary>
        /// ʮ�����
        /// </summary>
        QuarterHour = 15,
        /// <summary>
        /// ��Сʱ
        /// </summary>
        HalfHour = 30,
        /// <summary>
        /// һСʱ
        /// </summary>
        OneHour = 60,
        /// <summary>
        /// ��Сʱ
        /// </summary>
        TwoHour = 120,
        /// <summary>
        /// ����(12Сʱ)
        /// </summary>
        HalfDay = 720
    }
}
