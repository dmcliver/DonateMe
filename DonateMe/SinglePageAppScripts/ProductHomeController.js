app.controller("ProductHomeController", [

    "$scope", "ProductRepository", function ($scope, productRepository) {

        $scope.message = "";
        $scope.info = null;
        $scope.prodName = null;
        $scope.prodDesc = null;
        $scope.prodModel = null;
        $scope.itemCategories = null;
        $scope.selectedCategory = null;

        $scope.grabData = function() {

            if(!$scope.itemCategories)
                productRepository.getItemCategories($scope.onItemCategoriesReceived);
        }

        $scope.onItemCategoriesReceived = function (categories) {
            $scope.itemCategories = categories;
        }

        $scope.update = function(id) {
            productRepository.getByCategoryId(id, $scope.onProductsReceived);
        };

        $scope.submitForm = function () {

            $scope.prodName += "?";
            var formData = new FormData();
            formData.append("Name", $scope.prodDesc);
            formData.append("Description", $scope.prodModel);
            formData.append("Model", $scope.prodName);
            formData.append("Image", $scope.files[0]);
            productRepository.sendData(formData);
        };

        $scope.onProductsReceived = function(products) {

            if (products.length) {

                $scope.info = products;
                $scope.message = "";
            }
            else {

                $scope.info = null;
                $scope.message = "No products to display";
            }
        };

        $scope.files = [];

        $scope.$on("fileSelected", function (event, args) {

            $scope.$apply(function () {

                $scope.files.push(args.file);
            });
        });
    }
]);

app.directive("fileUpload", function() {

    return {

        scope: true, 
        link: function(scope, el, attrs) {

            el.bind("change", function (event) {

                var files = event.target.files;
                
                for (var i = 0; i < files.length; i++) {
                    scope.$emit("fileSelected", { file: files[i] });
                }
            });
        }
    }
});