"use strict";

function ProductRepository() {

    this.getProductsById = function (id, command) {

        $.getJSON("/Api/Item", { "id": id })
         .done(function (data) {

             command.execute(data);
         });
    }
}