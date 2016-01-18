app.directive("selectedTreeItemDirective", [

    function () {

        return {

            scope: false,
            controller: "ProductHomeController",
            replace: true,
            link: function (actualScope, element, attr) {

                element.on("changed.jstree", function (evt, el) {

                    actualScope.$apply(function () {
                        actualScope.update(el.node.id);
                    });
                });
            }
        };
    }
]);