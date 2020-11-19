using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CouchPotato.ApiUtil;

namespace CouchPotato.ApiUtil
{
    enum Provider
    {
        Netflix, AmazonPrime, Aniflix
    }

    static class ProviderMethods
    {
        //TODO
        private static IApi NETFLIX = new Api();
        private static IApi AMAZON_PRIME = new Api();
        private static IApi ANIFLIX = new Api();

        public static IApi getApi(this Provider provider)
        {
            switch (provider)
            {
                case Provider.Netflix: return NETFLIX;
                case Provider.AmazonPrime: return AMAZON_PRIME;
                case Provider.Aniflix: return ANIFLIX;
            }
            return null;
        }
    }
}
