app.controller("ProductHomeController", [

    "$scope", "ProductRepository", function ($scope, productRepository) {

        $scope.message = "";
        $scope.info = null;

        $scope.update = function (id) {
            productRepository.getByCategoryId(id, $scope.onProductsReceived);
        }

        $scope.onProductsReceived = function (products) {

            if (products.length) {

                $scope.info = products;
                $scope.message = "";
            }
            else {

                $scope.info = null;
                $scope.message = "No products to display";
            }
        }
    }
]);

