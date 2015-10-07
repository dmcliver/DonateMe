///<reference path="../../DonateMe/Scripts/jquery-1.9.1.js"/>
///<reference path="../../DonateMe/SinglePageAppScripts/ProductRepository.js"/>
///<reference path="Mocks/AjaxResponseMock.js"/>
///<reference path="../Scripts/jasmine.js"/>

describe("Product Repository", function() {

    "use strict";

    it("Should re-throw error if one is generated", function() {

        var errorMessage = "Something went horribly wrong";

        var ajaxResponse = new AjaxResponseMock();

        var cmd = jasmine.createSpyObj(null, ["execute"]);
        spyOn($, "getJSON");

        $.getJSON.andReturn(ajaxResponse);

        var repo = window.ProductRepository;
        expect(function () {

            repo.getProductsById(null, cmd);

            var failureCallback = ajaxResponse.getFailArgument();
            failureCallback(errorMessage);
        })
        .toThrow(errorMessage);

        expect(cmd.execute).not.toHaveBeenCalled();
    });

    it("Should call callback if received without error", function () {

        var testData = "Server side data";

        var ajaxResponse = new AjaxResponseMock();

        var cmd = jasmine.createSpyObj(null, ["execute"]);
        spyOn($, "getJSON");

        $.getJSON.andReturn(ajaxResponse);

        var repo = window.ProductRepository;
        expect(function() {

            repo.getProductsById(null, cmd);

            var successCallback = ajaxResponse.getDoneArgument();
            successCallback(testData);
        })
        .not.toThrow();

        expect(cmd.execute).toHaveBeenCalledWith(testData);
    });
});
