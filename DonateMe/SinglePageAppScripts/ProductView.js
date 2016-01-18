$(function () {

    $("#productTreeContainer").jstree({

        'core': {

            'data': {

                'url': "/Api/Navigator",
                'dataType': "json",
                'data': function (node) {
                    return { 'id': node.id };
                }
            }
        }
    });
});