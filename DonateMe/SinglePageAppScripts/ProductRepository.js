app.service("ProductRepository", [

    "$http", function ($http) {

        this.getByCategoryId = function(id, callback) {

            $http({
                method: "GET",
                url: "/Api/Item",
                params: { "id": id }
            })
            .then(function (succResp) {
                callback(succResp.data);
            },
            function (errResp) {
                throw new Error(errResp.status + " " + errResp.statusText + " from ProductRepository::getProductsById calling url:" + url);
            });
        };

        this.sendData = function (model) {

            $http({

                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined },
                method: "POST",
                url: "/Api/Item",
                data: model
            });
        };
    }
]);