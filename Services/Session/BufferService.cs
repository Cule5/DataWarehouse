using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;
using System.Threading.Tasks;
using Core.Domain.Common;
using Microsoft.AspNetCore.Http;
using Services.ETL;

namespace Services.Session
{
    public class BufferService:IBufferService
    {
        private List<transaction> _bufferedDataEShop=new List<transaction>();
        private List<transaction> _bufferedDataStationaryShop=new List<transaction>();
        private List<transaction> _bufferedDataTeleShop=new List<transaction>();
        
        
        public Task AddToBufferAsync(transaction transaction,EShopType shopType)
        {
            return Task.Factory.StartNew(() =>
            {
                if (shopType == EShopType.EShop)
                {
                    _bufferedDataEShop.Add(transaction);
                }
                else if (shopType == EShopType.StationaryShop) 
                {
                    _bufferedDataStationaryShop.Add(transaction);
                }
                else if (shopType == EShopType.TeleShop)
                {
                    _bufferedDataTeleShop.Add(transaction);
                }
                
            });
            
        }

        public Task<List<transaction>> GetBufferAsync(EShopType shopType)
        {
            return Task.Factory.StartNew<List<transaction>>(() =>
            {
                if(shopType==EShopType.EShop)
                    return new List<transaction>(_bufferedDataEShop);
                if(shopType==EShopType.StationaryShop)
                    return new List<transaction>(_bufferedDataStationaryShop);
                if(shopType==EShopType.TeleShop)
                    return new List<transaction>(_bufferedDataTeleShop);
                return null;

            });
        }

        public  Task ClearBufferAsync(EShopType shopType)
        {
            
            return Task.Factory.StartNew(() =>
            {
                if(shopType==EShopType.EShop)
                    _bufferedDataEShop.Clear();
                if(shopType==EShopType.StationaryShop)
                    _bufferedDataStationaryShop.Clear();
                if(shopType==EShopType.TeleShop)
                    _bufferedDataTeleShop.Clear();

            });
        }
    }
}
