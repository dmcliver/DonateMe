"use strict";

function ProductRepository(jq) {

    jq = jq || jQuery;

    this.getProductsById = function (id, command) {

        jq.getJSON("/Api/Item", { "id": id })
          .done(function (data) {
              command.execute(data);
          });
    }
}
