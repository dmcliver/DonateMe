var ProductRepositoryMock = (function () {

    "use strict";

    var getProductByIdCalls = [];

    return {

        getProductsById: function (id, cb) {
            getProductByIdCalls.push([id, cb]);
        },

        getProductsByIdCalls: function () {
            return getProductByIdCalls;
        }
    };
});