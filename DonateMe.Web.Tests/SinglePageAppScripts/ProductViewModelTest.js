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

        var message = "";
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
        var serverSideData = [{Name: "Guitar"}, {Name: "Amp"}, {Name: "Drums"}];
        viewModelCallback.execute(serverSideData);

        expect(info.length).toBe(3);
    });
});

