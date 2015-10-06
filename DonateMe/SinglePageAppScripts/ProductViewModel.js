"use strict";

function ProductViewModel(prodRepo) {

    prodRepo = prodRepo || new ProductRepository(jQuery);

    var self = this;

    self.info = ko.observableArray();

    self.message = ko.observable();

    function loadData(data) {

        self.info.removeAll();
        self.message("");

        if (data.length < 1) {

            self.message("No products to display");
            return;
        }

        for (var i = 0; i < data.length; i++) {
            self.info.push({ value: data[i].Name });
        }
    }

    self.getProducts = function (caller, elem, data) {
        prodRepo.getProductsById(data.node.id, {execute: loadData});
    }
}