///<reference path="../../DonateMe/Scripts/knockout-2.2.0.js"/>
///<reference path="../../DonateMe/SinglePageAppScripts/ProductViewModel.js"/>
///<reference path="../SinglePageAppScripts/ProductRepositoryMock.js"/>
///<reference path="../Scripts/jasmine.js"/>

describe("Product ViewModel", function() {

    it("Should display message if no data found", function() {

        var message = "";
        var selectedTreeNode = { node: { id: null } };
        var repo = ProductRepositoryMock();

        var viewModel = new ProductViewModel(repo);
        viewModel.message = function(m) {
            message = m;
        };
        viewModel.getProducts(null, null, selectedTreeNode);

        var productsByIdCalls = repo.getProductsByIdCalls();
        expect(productsByIdCalls.length).toBe(1);

        var viewModelCallback = productsByIdCalls[0][1];
        var serverSideData = [];
        viewModelCallback.execute(serverSideData);

        expect(message).toBe("No products to display");
    });

    it("Should update observable array with product info when data is present", function() {

        var firstModelName = "Guitar";
        var secondModelName = "Amp";
        var thirdModelName = "Drums";

        var message;
        var info = [];
        var selectedTreeNode = { node: { id: null } };
        var repo = ProductRepositoryMock();

        var viewModel = new ProductViewModel(repo);
        viewModel.info = {

            removeAll: function () { },
            push: function (obj) { info.push(obj); }
        };
        viewModel.message = function (m) {
            message = m;
        };
        viewModel.getProducts(null, null, selectedTreeNode);

        var productsByIdCalls = repo.getProductsByIdCalls();
        expect(productsByIdCalls.length).toBe(1);

        var viewModelCallback = productsByIdCalls[0][1];
        
        var serverSideData = [{Name: firstModelName}, {Name: secondModelName}, {Name: thirdModelName}];
        viewModelCallback.execute(serverSideData);

        expect(info.length).toBe(3);
        expect(info[0].value).toBe(firstModelName);
        expect(info[1].value).toBe(secondModelName);
        expect(info[2].value).toBe(thirdModelName);
        expect(message).toBeFalsy();
    });
});
