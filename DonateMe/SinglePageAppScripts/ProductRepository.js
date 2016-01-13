"use strict";

(function() {

    var self = {};

    self.getProductsById = function (id, command) {

        var url = "/Api/Item";

        $.getJSON(url, { "id": id })
         .done(function(data) {
             command.execute(data);
         })
         .fail(function(err) {
             throw new Error(err.status + " " + err.statusText + " from ProductRepository::getProductsById calling url:" + url);
         });
    }

    window.ProductRepository = self;

})();
