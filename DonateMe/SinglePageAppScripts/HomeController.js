app.controller("HomeController", [

    "$scope", "HomeRepository", function ($scope, homeRepository) {

        $scope.message = "";
        $scope.info = null;

        $scope.update = function (id) {
            homeRepository.fetchData(id, $scope.onData);
        }

        $scope.onData = function (result) {

            if (result.length) {

                $scope.info = result;
                $scope.message = "";
            }
            else {

                $scope.message = "No products to display";
                $scope.info = null;
            }
        }
    }
]);

