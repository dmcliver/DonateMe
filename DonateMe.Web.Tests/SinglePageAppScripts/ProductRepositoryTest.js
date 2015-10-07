///<reference path="../../DonateMe/SinglePageAppScripts/ProductRepository.js"/>
///<reference path="Mocks/AjaxResponseMock.js"/>
///<reference path="Mocks/JsQueryMock.js"/>
///<reference path="../Scripts/jasmine.js"/>

describe("Product Repository", function() {

    "use strict";

    it("Should re-throw error if one is generated", function() {

        var errorMessage = "Something went horribly wrong";

        var cmd = jasmine.createSpyObj(null, ["execute"]);

        var repo = new ProductRepository(JsQueryMock);
        expect(function () {

            repo.getProductsById(null, cmd);

            var failureCallback = JsQueryMock.getJsonFailArgument();
            failureCallback(errorMessage);
        })
        .toThrow(errorMessage);

        expect(cmd.execute).not.toHaveBeenCalled();
    });

    it("Should call callback if received without error", function () {

        var testData = "Server side data";

        var cmd = jasmine.createSpyObj(null, ["execute"]);

        var repo = new ProductRepository(JsQueryMock);
        expect(function() {

            repo.getProductsById(null, cmd);

            var successCallback = JsQueryMock.getJsonDoneArgument();
            successCallback(testData);
        })
        .not.toThrow();

        expect(cmd.execute).toHaveBeenCalledWith(testData);
    });
});

