using System;
using System.Linq.Expressions;
using System.Reflection;
using Obs = System.Reactive.Linq.Observable;

namespace rxps
{
    internal static class LateBoundTypeCalls
    {
        private static readonly object[] ZeroParams = new object[0];

        private static MethodInfo GetMethodInfo<T>(Expression<Func<T>> expr)
        {
            var method = (MethodInfo)((MemberExpression)expr.Body).Member;

            return method.IsGenericMethod ? method.GetGenericMethodDefinition() : method;
        }

        public static class Observable
        {
            private static readonly MethodInfo Observable_OfType = GetMethodInfo(() => Obs.OfType<object>(null));

            public static dynamic OfType(Type typeParameter, IObservable<object> source)
            {
                return Observable_OfType.MakeGenericMethod(typeParameter).Invoke(null, new object[] { source });
            }
        }
    }
}
