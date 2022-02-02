﻿using SupplementStore.Application.Args;
using SupplementStore.Application.Results;

namespace SupplementStore.Application.Services {

    public interface IProductProvider {
        ProductProviderResult Load(ProductProviderArgs args);
    }
}
