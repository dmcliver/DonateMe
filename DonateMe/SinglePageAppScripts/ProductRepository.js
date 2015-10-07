"use strict";

(function() {

    var self = {};

    self.getProductsById = function(id, command) {

        $.getJSON("/Api/Item", { "id": id })
         .done(function(data) {
                command.execute(data);
         })
         .fail(function(err) {
             throw new Error(err);
         });
    }

    window.ProductRepository = self;

})();
