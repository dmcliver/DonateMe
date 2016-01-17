app.controller("HomeController", [

    "$scope", "$document", function ($scope, $document) {

        $scope.message = "yo";

        $scope.update = function (x) {
            $scope.message = x;
        }

        $scope.sayHello = function() {
            $scope.message = "hi";
        }
    }
]);

app.directive("treeLink", [

    "$rootScope", function ($rootScope) {

        return {

            scope: false,
            controller : "HomeController",
            replace: true,
            link: function (s, element, attr) {

                element.on("changed.jstree", function (evt) {

                    s.$apply(function() {
                        s.update("?x");
                    });
                });
            }
        };
    }
]);