///<reference path="../../DonateMe/Scripts/jquery-1.9.1.js"/>
///<reference path="../../DonateMe/SinglePageAppScripts/ProductRepository.js"/>
///<reference path="Mocks/AjaxResponseMock.js"/>
///<reference path="../Scripts/jasmine.js"/>

describe("Product Repository", function() {

    "use strict";
        
    it("Should re-throw error if one is generated", function() {

        var errorResponse = { status: 404, statusText: "error" };

        var ajaxResponse = new AjaxResponseMock();

        var command = jasmine.createSpyObj("ICommand", ["execute"]);
        spyOn($, "getJSON");

        $.getJSON.andReturn(ajaxResponse);

        var repo = window.ProductRepository;
        expect(function () {

            repo.getProductsById(null, command);

            var failureCallback = ajaxResponse.getFailArgument();
            failureCallback(errorResponse);
        })
        .toThrow("404 error from ProductRepository::getProductsById calling url:/Api/Item");

        expect(command.execute).not.toHaveBeenCalled();
    });

    it("Should call callback if received without error", function () {

        var testData = "Server side data";

        var ajaxResponse = new AjaxResponseMock();

        var command = jasmine.createSpyObj("ICommand", ["execute"]);
        spyOn($, "getJSON");

        $.getJSON.andReturn(ajaxResponse);

        var repo = window.ProductRepository;
        expect(function() {

            repo.getProductsById(null, command);

            var successCallback = ajaxResponse.getDoneArgument();
            successCallback(testData);
        })
        .not.toThrow();

        expect(command.execute).toHaveBeenCalledWith(testData);
    });
});
