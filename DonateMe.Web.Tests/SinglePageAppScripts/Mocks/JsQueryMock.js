var JsQueryMock = (function () {

    "use strict";

    var ajaxResponse = new AjaxResponseMock();

    return {
        getJSON: function(uri, data) {
            return ajaxResponse;
        },
        getJsonDoneArgument: function() {
            return ajaxResponse.getDoneArgument();
        },
        getJsonFailArgument: function() {
            return ajaxResponse.getFailArgument();
        },
        getJsonCompleteArgument: function() {
            return ajaxResponse.getCompleteArgument();
        }
    };
})();