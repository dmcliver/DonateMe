app.service("HomeRepository", [

    "$http", function ($http) {

        this.fetchData = function(id, callback) {

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
    }
]);