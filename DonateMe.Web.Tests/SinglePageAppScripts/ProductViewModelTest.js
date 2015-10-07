///<reference path="../../DonateMe/Scripts/knockout-2.2.0.js"/>
///<reference path="../../DonateMe/SinglePageAppScripts/ProductRepository.js"/>
///<reference path="../../DonateMe/SinglePageAppScripts/ProductViewModel.js"/>
///<reference path="../Scripts/jasmine.js"/>

describe("Product ViewModel", function() {

    "use strict";

    it("Should display message if no data found", function() {

        var selectedTreeNode = { node: { id: null } };
        spyOn(window.ProductRepository, "getProductsById");

        var viewModel = window.ProductViewModel;
        viewModel.info = jasmine.createSpyObj("info", ["removeAll", "push"]);
        viewModel.message = jasmine.createSpy("message");
        viewModel.getProducts(null, null, selectedTreeNode);

        var productsByIdCalls = window.ProductRepository.getProductsById.calls;
        expect(productsByIdCalls.length).toBe(1);

        var viewModelCallback = productsByIdCalls[0].args[1];
        var serverSideData = [];
        viewModelCallback.execute(serverSideData);

        expect(viewModel.info.removeAll.calls.length).toBe(1);
        expect(viewModel.message.calls.length).toBe(2);
        expect(viewModel.message.calls[1].args[0]).toBe("No products to display");
        expect(viewModel.info.push.calls.length).toBe(0);
    });

    it("Should update observable array with product info when data is present", function() {

        var firstModelName = "Guitar";
        var secondModelName = "Amp";
        var thirdModelName = "Drums";

        var selectedTreeNode = { node: { id: null } };

        spyOn(window.ProductRepository, "getProductsById");

        var viewModel = window.ProductViewModel;
        viewModel.info = jasmine.createSpyObj("info", ["removeAll", "push"]);
        viewModel.message = jasmine.createSpy("message");
        viewModel.getProducts(null, null, selectedTreeNode);

        var productsByIdCalls = window.ProductRepository.getProductsById.calls;
        expect(productsByIdCalls.length).toBe(1);

        var viewModelCallback = productsByIdCalls[0].args[1];
        
        var serverSideData = [{Name: firstModelName}, {Name: secondModelName}, {Name: thirdModelName}];
        viewModelCallback.execute(serverSideData);

        expect(viewModel.info.removeAll.calls.length).toBe(1);
        expect(viewModel.info.push.calls.length).toBe(3);
        expect(viewModel.info.push.calls[0].args[0].value).toBe(firstModelName);
        expect(viewModel.info.push.calls[1].args[0].value).toBe(secondModelName);
        expect(viewModel.info.push.calls[2].args[0].value).toBe(thirdModelName);
        expect(viewModel.message.calls.length).toBe(1);
    });
});
