﻿using Grand.Core.Caching;
using Grand.Core.Domain.Catalog;
using Grand.Core.Domain.Configuration;
using Grand.Core.Domain.Discounts;
using Grand.Core.Domain.Orders;
using Grand.Core.Events;
using Grand.Services.Events;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Grand.Services.Catalog.Cache
{
    /// <summary>
    /// Price cache event consumer (used for caching of prices)
    /// </summary>
    public partial class PriceCacheEventConsumer :
        //settings
        IConsumer<EntityUpdated<Setting>>,
        //categories
        IConsumer<EntityInserted<Category>>,
        IConsumer<EntityUpdated<Category>>,
        IConsumer<EntityDeleted<Category>>,
        //manufacturers
        IConsumer<EntityInserted<Manufacturer>>,
        IConsumer<EntityUpdated<Manufacturer>>,
        IConsumer<EntityDeleted<Manufacturer>>,
        //discounts
        IConsumer<EntityInserted<Discount>>,
        IConsumer<EntityUpdated<Discount>>,
        IConsumer<EntityDeleted<Discount>>,
        //product categories
        IConsumer<EntityInserted<ProductCategory>>,
        IConsumer<EntityUpdated<ProductCategory>>,
        IConsumer<EntityDeleted<ProductCategory>>,
        //product manufacturers
        IConsumer<EntityInserted<ProductManufacturer>>,
        IConsumer<EntityUpdated<ProductManufacturer>>,
        IConsumer<EntityDeleted<ProductManufacturer>>,

        //products
        IConsumer<EntityInserted<Product>>,
        IConsumer<EntityUpdated<Product>>,
        IConsumer<EntityDeleted<Product>>,
        //tier prices
        IConsumer<EntityInserted<TierPrice>>,
        IConsumer<EntityUpdated<TierPrice>>,
        IConsumer<EntityDeleted<TierPrice>>,
        //orders
        IConsumer<EntityInserted<Order>>,
        IConsumer<EntityUpdated<Order>>,
        IConsumer<EntityDeleted<Order>>
    {
        /// <summary>
        /// Key for product prices
        /// </summary>
        /// <remarks>
        /// {0} : product id
        /// {1} : additional charge
        /// {2} : include discounts (true, false)
        /// {3} : quantity
        /// {4} : roles of the current user
        /// {5} : current store ID
        /// </remarks>
        public const string PRODUCT_PRICE_MODEL_KEY = "Grand.totals.productprice-{0}-{1}-{2}-{3}-{4}-{5}";
        public const string PRODUCT_PRICE_PATTERN_KEY = "Grand.totals.productprice";

        /// <summary>
        /// Key for category IDs of a discount
        /// </summary>
        /// <remarks>
        /// {0} : discount id
        /// {1} : roles of the current user
        /// {2} : current store ID
        /// </remarks>
        public const string DISCOUNT_CATEGORY_IDS_MODEL_KEY = "Grand.totals.discount.categoryids-{0}-{1}-{2}";
        public const string DISCOUNT_CATEGORY_IDS_PATTERN_KEY = "Grand.totals.discount.categoryids";

        /// <summary>
        /// Key for manufacturer IDs of a discount
        /// </summary>
        /// <remarks>
        /// {0} : discount id
        /// {1} : roles of the current user
        /// {2} : current store ID
        /// </remarks>
        public const string DISCOUNT_MANUFACTURER_IDS_MODEL_KEY = "Grand.totals.discount.manufacturerids-{0}-{1}-{2}";
        public const string DISCOUNT_MANUFACTURER_IDS_PATTERN_KEY = "Grand.totals.discount.manufacturerids";

        /// <summary>
        /// Key for category IDs of a product
        /// </summary>
        /// <remarks>
        /// {0} : product id
        /// {1} : roles of the current user
        /// {2} : current store ID
        /// </remarks>
        public const string DISCOUNT_PRODUCT_CATEGORY_IDS_MODEL_KEY = "Grand.totals.product.categoryids-{0}-{1}-{2}";
        public const string DISCOUNT_PRODUCT_CATEGORY_IDS_PATTERN_KEY = "Grand.totals.product.categoryids";

        /// <summary>
        /// Key for manufacturer IDs of a product
        /// </summary>
        /// <remarks>
        /// {0} : product id
        /// {1} : roles of the current user
        /// {2} : current store ID
        /// </remarks>
        public const string DISCOUNT_PRODUCT_MANUFACTURER_IDS_MODEL_KEY = "Grand.totals.product.manufacturerids-{0}-{1}-{2}";
        public const string DISCOUNT_PRODUCT_MANUFACTURER_IDS_PATTERN_KEY = "Grand.totals.product.manufacturerids";

        private readonly ICacheManager _cacheManager;

        public PriceCacheEventConsumer(IServiceProvider serviceProvider)
        {
            _cacheManager = serviceProvider.GetRequiredService<ICacheManager>();
        }

        //settings
        public async Task HandleEvent(EntityUpdated<Setting> eventMessage)
        {
            await _cacheManager.RemoveByPattern(PRODUCT_PRICE_PATTERN_KEY);
            await _cacheManager.RemoveByPattern(DISCOUNT_CATEGORY_IDS_PATTERN_KEY);
            await _cacheManager.RemoveByPattern(DISCOUNT_MANUFACTURER_IDS_PATTERN_KEY);
            await _cacheManager.RemoveByPattern(DISCOUNT_PRODUCT_CATEGORY_IDS_PATTERN_KEY);
            await _cacheManager.RemoveByPattern(DISCOUNT_PRODUCT_MANUFACTURER_IDS_PATTERN_KEY);
        }

        //categories
        public async Task HandleEvent(EntityInserted<Category> eventMessage)
        {
            await _cacheManager.RemoveByPattern(DISCOUNT_CATEGORY_IDS_PATTERN_KEY);
            await _cacheManager.RemoveByPattern(DISCOUNT_PRODUCT_CATEGORY_IDS_PATTERN_KEY);
        }
        public async Task HandleEvent(EntityUpdated<Category> eventMessage)
        {
            await _cacheManager.RemoveByPattern(DISCOUNT_CATEGORY_IDS_PATTERN_KEY);
            await _cacheManager.RemoveByPattern(DISCOUNT_PRODUCT_CATEGORY_IDS_PATTERN_KEY);
        }
        public async Task HandleEvent(EntityDeleted<Category> eventMessage)
        {
            await _cacheManager.RemoveByPattern(DISCOUNT_CATEGORY_IDS_PATTERN_KEY);
            await _cacheManager.RemoveByPattern(DISCOUNT_PRODUCT_CATEGORY_IDS_PATTERN_KEY);
        }

        //manufacturers
        public async Task HandleEvent(EntityInserted<Manufacturer> eventMessage)
        {
            await _cacheManager.RemoveByPattern(DISCOUNT_MANUFACTURER_IDS_PATTERN_KEY);
            await _cacheManager.RemoveByPattern(DISCOUNT_PRODUCT_MANUFACTURER_IDS_PATTERN_KEY);
        }
        public async Task HandleEvent(EntityUpdated<Manufacturer> eventMessage)
        {
            await _cacheManager.RemoveByPattern(DISCOUNT_MANUFACTURER_IDS_PATTERN_KEY);
            await _cacheManager.RemoveByPattern(DISCOUNT_PRODUCT_MANUFACTURER_IDS_PATTERN_KEY);
        }
        public async Task HandleEvent(EntityDeleted<Manufacturer> eventMessage)
        {
            await _cacheManager.RemoveByPattern(DISCOUNT_MANUFACTURER_IDS_PATTERN_KEY);
            await _cacheManager.RemoveByPattern(DISCOUNT_PRODUCT_MANUFACTURER_IDS_PATTERN_KEY);
        }

        //discounts
        public async Task HandleEvent(EntityInserted<Discount> eventMessage)
        {
            await _cacheManager.RemoveByPattern(DISCOUNT_CATEGORY_IDS_PATTERN_KEY);
            await _cacheManager.RemoveByPattern(DISCOUNT_MANUFACTURER_IDS_PATTERN_KEY);
        }
        public async Task HandleEvent(EntityUpdated<Discount> eventMessage)
        {
            await _cacheManager.RemoveByPattern(DISCOUNT_CATEGORY_IDS_PATTERN_KEY);
            await _cacheManager.RemoveByPattern(DISCOUNT_MANUFACTURER_IDS_PATTERN_KEY);
        }
        public async Task HandleEvent(EntityDeleted<Discount> eventMessage)
        {
            await _cacheManager.RemoveByPattern(DISCOUNT_CATEGORY_IDS_PATTERN_KEY);
            await _cacheManager.RemoveByPattern(DISCOUNT_MANUFACTURER_IDS_PATTERN_KEY);
        }

        //product categories
        public async Task HandleEvent(EntityInserted<ProductCategory> eventMessage)
        {
            await _cacheManager.RemoveByPattern(PRODUCT_PRICE_PATTERN_KEY);
            await _cacheManager.RemoveByPattern(DISCOUNT_PRODUCT_CATEGORY_IDS_PATTERN_KEY);
        }
        public async Task HandleEvent(EntityUpdated<ProductCategory> eventMessage)
        {
            await _cacheManager.RemoveByPattern(PRODUCT_PRICE_PATTERN_KEY);
            await _cacheManager.RemoveByPattern(DISCOUNT_PRODUCT_CATEGORY_IDS_PATTERN_KEY);
        }
        public async Task HandleEvent(EntityDeleted<ProductCategory> eventMessage)
        {
            await _cacheManager.RemoveByPattern(PRODUCT_PRICE_PATTERN_KEY);
            await _cacheManager.RemoveByPattern(DISCOUNT_PRODUCT_CATEGORY_IDS_PATTERN_KEY);
        }

        //product manufacturers
        public async Task HandleEvent(EntityInserted<ProductManufacturer> eventMessage)
        {
            await _cacheManager.RemoveByPattern(PRODUCT_PRICE_PATTERN_KEY);
            await _cacheManager.RemoveByPattern(DISCOUNT_PRODUCT_MANUFACTURER_IDS_PATTERN_KEY);
        }
        public async Task HandleEvent(EntityUpdated<ProductManufacturer> eventMessage)
        {
            await _cacheManager.RemoveByPattern(PRODUCT_PRICE_PATTERN_KEY);
            await _cacheManager.RemoveByPattern(DISCOUNT_PRODUCT_MANUFACTURER_IDS_PATTERN_KEY);
        }
        public async Task HandleEvent(EntityDeleted<ProductManufacturer> eventMessage)
        {
            await _cacheManager.RemoveByPattern(PRODUCT_PRICE_PATTERN_KEY);
            await _cacheManager.RemoveByPattern(DISCOUNT_PRODUCT_MANUFACTURER_IDS_PATTERN_KEY);
        }

        //products
        public async Task HandleEvent(EntityInserted<Product> eventMessage)
        {
            await _cacheManager.RemoveByPattern(PRODUCT_PRICE_PATTERN_KEY);
        }
        public async Task HandleEvent(EntityUpdated<Product> eventMessage)
        {
            await _cacheManager.RemoveByPattern(PRODUCT_PRICE_PATTERN_KEY);
        }
        public async Task HandleEvent(EntityDeleted<Product> eventMessage)
        {
            await _cacheManager.RemoveByPattern(PRODUCT_PRICE_PATTERN_KEY);
        }

        //tier prices
        public async Task HandleEvent(EntityInserted<TierPrice> eventMessage)
        {
            await _cacheManager.RemoveByPattern(PRODUCT_PRICE_PATTERN_KEY);
        }
        public async Task HandleEvent(EntityUpdated<TierPrice> eventMessage)
        {
            await _cacheManager.RemoveByPattern(PRODUCT_PRICE_PATTERN_KEY);
        }
        public async Task HandleEvent(EntityDeleted<TierPrice> eventMessage)
        {
            await _cacheManager.RemoveByPattern(PRODUCT_PRICE_PATTERN_KEY);
        }

        //orders
        public async Task HandleEvent(EntityInserted<Order> eventMessage)
        {
            await _cacheManager.RemoveByPattern(PRODUCT_PRICE_PATTERN_KEY);
        }
        public async Task HandleEvent(EntityUpdated<Order> eventMessage)
        {
            await _cacheManager.RemoveByPattern(PRODUCT_PRICE_PATTERN_KEY);
        }
        public async Task HandleEvent(EntityDeleted<Order> eventMessage)
        {
            await _cacheManager.RemoveByPattern(PRODUCT_PRICE_PATTERN_KEY);
        }
    }
}
