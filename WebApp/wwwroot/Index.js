

function submitForm() {
    var submitionStr = "/Home/Sort?";
    var a_products = document.getElementsByClassName("productTitle");
    for (var i = 0; i < a_products.length; i++) {
        if (i != 0) submitionStr += "&";
        submitionStr += "products=" + a_products[i].id;
    }
    
    var productlines_count = 0;
    var inputs = document.getElementsByTagName("input");
    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].type == "checkbox") {
            if (inputs[i].name == "productLines") {
                if (inputs[i].checked == true) {
                    if (productlines_count != 0) submitionStr += "&";
                    else submitionStr += "&&";
                    submitionStr += "productLines=" + inputs[i].value;
                    productlines_count++;
                }
            }
        }
    }
    submitionStr += "&&";
    var price_sorttype_count = 0;
    var inputs = document.getElementsByTagName("input");
    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].type == "checkbox") {
            if (inputs[i].name == "price_sorttypeids") {
                if (inputs[i].checked == true) {
                    if (price_sorttype_count != 0) submitionStr += "&";
                    else submitionStr += "&&";
                    submitionStr += "price_sorttypeids=" + inputs[i].value;
                    price_sorttype_count++;
                }
            }
        }
    }
    submitionStr += "&&";
    var age_sorttype_count = 0;
    var inputs = document.getElementsByTagName("input");
    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].type == "checkbox") {
            if (inputs[i].name == "age_sorttypeids") {
                if (inputs[i].checked == true) {
                    if (age_sorttype_count != 0) submitionStr += "&";
                    else submitionStr += "&&";
                    submitionStr += "age_sorttypeids=" + inputs[i].value;
                    age_sorttype_count++;
                }
            }
        }
    }
    submitionStr += "&&";
    var itemsc_sorttype_count = 0;
    var inputs = document.getElementsByTagName("input");
    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].type == "checkbox") {
            if (inputs[i].name == "itemsc_sorttypeids") {
                if (inputs[i].checked == true) {
                    if (itemsc_sorttype_count != 0) submitionStr += "&";
                    else submitionStr += "&&";
                    submitionStr += "itemsc_sorttypeids=" + inputs[i].value;
                    itemsc_sorttype_count++;
                }
            }
        }
    }
    submitionStr += "&&";
    var figuresc_sorttype_count = 0;
    var inputs = document.getElementsByTagName("input");
    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].type == "checkbox") {
            if (inputs[i].name == "figuresc_sorttypeids") {
                if (inputs[i].checked == true) {
                    if (figuresc_sorttype_count != 0) submitionStr += "&";
                    else submitionStr += "&&";
                    submitionStr += "figuresc_sorttypeids=" + inputs[i].value;
                    figuresc_sorttype_count++;
                }
            }
        }
    }
    submitionStr += "&&";
    var categories_count = 0;
    var inputs = document.getElementsByTagName("input");
    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].type == "checkbox") {
            if (inputs[i].name == "categoryids") {
                if (inputs[i].checked == true) {
                    if (categories_count != 0) submitionStr += "&";
                    else submitionStr += "&&";
                    submitionStr += "categoryids=" + inputs[i].value;
                    categories_count++;
                }
            }
        }
    }
    
    document.formsort.action = submitionStr;
    document.getElementById("formsort").submit();
}

function SavePriceSort(newsort) {
    var submitStr = "/Home/EditPriceSort?";    
    var inputs = document.getElementsByTagName("input");
    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].name == "pricemin") {
            submitStr += "id=" + inputs[i].id;           
        }       
    }
    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].name == "pricemin") {          
            submitStr += "&&pricemin=" + inputs[i].value.toString();
        }
        else if (inputs[i].name == "pricemax") {
            submitStr += "&&pricemax=" + inputs[i].value.toString();           
        }
    }    
    if (newsort != null) {
        submitStr += "&&newsort=1";
    }
    var forms = document.getElementsByTagName("form");
    for (var i = 0; i < forms.length; i++) {
        if (forms[i].id == "masterform") {
            forms[i].action = submitStr;
            forms[i].submit();
        }
    }
}


function SaveItemsSort(newsort) {
    var submitStr = "/Home/EditItemsSort?";
    var inputs = document.getElementsByTagName("input");
    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].name == "itemscmin") {
            submitStr += "id=" + inputs[i].id;
        }
    }
    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].name == "itemscmin") {
            submitStr += "&&itemscmin=" + inputs[i].value.toString();
        }
        else if (inputs[i].name == "itemscmax") {
            submitStr += "&&itemscmax=" + inputs[i].value.toString();
        }
    }
    if (newsort != null) {
        submitStr += "&&newsort=1";
    }
    var forms = document.getElementsByTagName("form");
    for (var i = 0; i < forms.length; i++) {
        if (forms[i].id == "masterform") {
            forms[i].action = submitStr;
            forms[i].submit();
        }
    }
}

function SaveFiguresSort(newsort) {
    var submitStr = "/Home/EditFiguresSort?";
    var inputs = document.getElementsByTagName("input");
    for (var i = 0; i < inputs.length; i++) {        
        if (inputs[i].name == "figuresc") {
            submitStr += "id=" + inputs[i].id;
        }
    }
    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].name == "figurescmin") {
            submitStr += "&&figurescmin=" + inputs[i].value.toString();
        }
        else if (inputs[i].name == "figurescmax") {
            submitStr += "&&figurescmax=" + inputs[i].value.toString();
        }
        else if (inputs[i].name == "figuresc") {
            submitStr += "&&figuresc=" + inputs[i].value.toString();
        }
    }
    if (newsort != null) {
        submitStr += "&&newsort=1";
    }
    var forms = document.getElementsByTagName("form");
    for (var i = 0; i < forms.length; i++) {
        if (forms[i].id == "masterform") {
            forms[i].action = submitStr;
            forms[i].submit();
        }
    }
}


function SaveAgeSort(newsort) {
    var submitStr = "/Home/EditAgeSort?";
    var inputs = document.getElementsByTagName("input");
    for (var i = 0; i < inputs.length; i++) {        
        if (inputs[i].name == "age") {
            submitStr += "id=" + inputs[i].id;
        }
    }
    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].name == "agemin") {
            submitStr += "&&agemin=" + inputs[i].value.toString();
        }
        else if (inputs[i].name == "agemax") {
            submitStr += "&&agemax=" + inputs[i].value.toString();
        }
        else if (inputs[i].name == "age") {
            submitStr += "&&age=" + inputs[i].value.toString();
        }
    }
    if (newsort != null) {
        submitStr += "&&newsort=1";
    }
    var forms = document.getElementsByTagName("form");
    for (var i = 0; i < forms.length; i++) {
        if (forms[i].id == "masterform") {
            forms[i].action = submitStr;
            forms[i].submit();
        }
    }    
}

function SaveCategorySort(newsort) {
    var submitStr = "/Home/EditCategorySort?";
    var inputs = document.getElementsByTagName("input");
    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].name == "categ") {
            submitStr += "id=" + inputs[i].id;
        }
    }
    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].name == "categ") {
            submitStr += "&&categ=" + inputs[i].value.toString();
        }        
    }
    if (newsort != null) {
        submitStr += "&&newsort=1";
    }
    var forms = document.getElementsByTagName("form");
    for (var i = 0; i < forms.length; i++) {
        if (forms[i].id == "masterform") {
            forms[i].action = submitStr;
            forms[i].submit();
        }
    }
}

