
window.onload = function () {
    checkedopacity(null);
}

function checkedopacity(li) {  
    var selectedli = "";
    if (li != null)
        selectedli = li;
    var inputs = document.getElementsByTagName("input");
    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].className == "radio") {
            if (inputs[i].id == "age") {
                if ((inputs[i].checked == true && selectedli == "age") || (inputs[i].checked == true && selectedli == "")) {  
                    found = "age";
                    var lis = document.getElementsByTagName("li");
                    for (var k = 0; k < lis.length; k++) {
                        if (lis[k].id == "li-agemin") {
                            lis[k].style.opacity = 0.5;
                        }
                    }                    
                    lis = document.getElementsByTagName("li");
                    for (var k = 0; k < lis.length; k++) {
                        if (lis[k].id == "li-agemax") {
                            lis[k].style.opacity = 0.5;
                        }
                    }
                }
                else{
                    inputs[i].checked = false;
                    var lis = document.getElementsByTagName("li");
                    for (var k = 0; k < lis.length; k++) {
                        if (lis[k].id == "li-agemin") {
                            lis[k].style.opacity = 1;
                        }
                    }
                    lis = document.getElementsByTagName("li");
                    for (var k = 0; k < lis.length; k++) {
                        if (lis[k].id == "li-agemax") {
                            lis[k].style.opacity = 1;
                        }
                    }
                }
            }
            else if (inputs[i].id == "agerange") {
                if ((inputs[i].checked == true && selectedli == "agerange") || (inputs[i].checked == true && selectedli == "")) {
                    found = "agerange";
                    var lis = document.getElementsByTagName("li");
                    for (var k = 0; k < lis.length; k++) {
                        if (lis[k].id == "li-age") {
                            lis[k].style.opacity = 0.5;
                        }
                    }
                }
                else {
                    inputs[i].checked = false;
                    var lis = document.getElementsByTagName("li");
                    for (var k = 0; k < lis.length; k++) {
                        if (lis[k].id == "li-age") {
                            lis[k].style.opacity = 1;
                        }
                    }
                }
            }
        }
    }
}

function CollectInfo() {
    var submitStr = "";

    var p = document.getElementsByTagName("p");
    var productid;
    for (var i = 0; i < p.length; i++) {
        if (p[i].className == "p-title")
            productid = p[i].id;
    }
    submitStr += "productid=" + productid;

    var inputs = document.getElementsByTagName("input");
    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].name == "productline") {
            submitStr += "&&productline=" + inputs[i].value;
        }
    }

    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].className == "radio") {
            if (inputs[i].checked == true) {
                if (inputs[i].id == "age") {
                    for (var j = 0; j < inputs.length; j++) {
                        if (inputs[j].name == "recommendedage") {
                            submitStr += "&&age=" + inputs[j].value.toString();
                        }
                    }
                }
                else if (inputs[i].id == "agerange") {
                    for (var j = 0; j < inputs.length; j++) {
                        if (inputs[j].name == "recommendedagemin") {
                            submitStr += "&&agemin=" + inputs[j].value.toString();
                        }
                        else if (inputs[j].name == "recommendedagemax") {
                            submitStr += "&&agemax=" + inputs[j].value.toString();
                        }
                    }
                }
            }
        }
    }

    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].name == "itemscount") {
            submitStr += "&&itemsc=" + inputs[i].value.toString();
        }
        else if (inputs[i].name == "figurescount") {
            submitStr += "&&figuresc=" + inputs[i].value.toString();
        }
    }

    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].name == "title") {
            submitStr += "&&title=" + inputs[i].value;
        }
    }

    var textareas = document.getElementsByTagName("textarea");
    for (var i = 0; i < textareas.length; i++) {
        if (textareas[i].name == "description") {
            submitStr += "&&description=" + textareas[i].value;
        }
    }

    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].name == "price") {
            submitStr += "&&price=" + inputs[i].value.toString();
        }
    }

    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].name == "thumbnail") {
            submitStr += "&&thumbnail=" + inputs[i].value;
        }
    }

    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].name == "bigpicture") {
            submitStr += "&&bigpicture=" + inputs[i].value;
        }
    }

    return submitStr;
}




function addcategory() {
    var submitStr = "/Home/AddCategoryToProduct?" + CollectInfo();
  
    var count = 0;
    var IsEmptyOption = false;
    var selects = document.getElementsByTagName("select");
    for (var i = 0; i < selects.length; i++) {
        if (selects[i].name = "categories") {
            for (var j = 0; j < selects[i].options.length; j++) {
                if (selects[i].options[j].selected == true) {
                    if (selects[i].options[j].id != "null") {
                        if (count == 0) submitStr += "&&";
                        else submitStr += "&";
                        submitStr += "categories=" + selects[i].options[j].id;
                        count++;
                    }
                    else IsEmptyOption = true;
                }
            }
        }
    }

    //var inputs = document.getElementsByTagName("input");
    //for (var i = 0; i < inputs.length; i++) {
    //    if (inputs[i].name == "productline") {
    //        inputs[i].value = CollectInfo();
    //    }
    //}   

   

    if (!IsEmptyOption) {        
        document.getElementById("addcateg").action = submitStr;
        document.getElementById("addcateg").submit();
    }

    //document.addcateg.action = submitStr;
}


function removecategory(form) {
    var submitStr = "/Home/RemoveCategoryFromProduct?" + CollectInfo();

    
    var IsEmptyOption = false;
    var count = 0;
    var liid = "li" + form.id.substring(4, form.id.length);
    var lis = document.getElementsByClassName("c-name");
    for (var i = 0; i < lis.length; i++) {
        if (lis[i].id != liid) {
            var lielem = lis[i].getElementsByTagName("select");
            for (var j = 0; j < lielem.length; j++) {
                if (lielem[j].name == "categories") {
                    for (var k = 0; k < lielem[j].options.length; k++) {
                        if (lielem[j].options[k].id != "null") {
                            if (lielem[j].options[k].selected == true) {                                
                                if (count == 0) submitStr += "&&";
                                else submitStr += "&";
                                submitStr += "categories=" + lielem[j].options[k].id;
                                count++;                                                                 
                            }
                        }
                        else IsEmptyOption = true;
                    }
                }
            }
        }
    } 

    if (form.id == "addcateg")
        IsEmptyOption = false;
   
    if (IsEmptyOption)
        submitStr += "&&nullcateg=true";
  
    form.action = submitStr;
    form.submit();
}


function removeemptycategory() {
    var submitStr = "/Home/RemoveCategoryFromProduct?" + CollectInfo();
    
    var count = 0;
    var lis = document.getElementsByClassName("c-name");
    for (var i = 0; i < lis.length; i++) {
        if (lis[i].id.substring(0, 2) == "li") {
            var selects = lis[i].getElementsByTagName("select");
            for (var j = 0; j < selects.length; j++) {
                if (selects[j].name == "categories") {
                    for (var k = 0; k < selects[j].options.length; k++) {
                        if (selects[j].options[k].selected == true) {
                            if (count == 0) submitStr += "&&";
                            else submitStr += "&";
                            submitStr += "categories=" + selects[j].options[k].id;
                            count++;
                        }
                    }
                }
            }
        }
    }

    var form = document.getElementById("addcateg");
    form.action = submitStr;
    form.submit();
}

function savechanges(newpro) {
    var submitStr = "/Home/SaveChanges?" + CollectInfo();
    
    var count = 0;    
    var selects = document.getElementsByTagName("select");
    for (var i = 0; i < selects.length; i++) {
        if (selects[i].name = "categories") {
            for (var j = 0; j < selects[i].options.length; j++) {
                if (selects[i].options[j].selected == true) {
                    if (selects[i].options[j].id != "null") {
                        if (count == 0) submitStr += "&&";
                        else submitStr += "&";
                        submitStr += "categories=" + selects[i].options[j].id;
                        count++;
                    }                    
                }
            }
        }
    }  
    if (newpro != null) {
        submitStr += "&&newpro=1";
    }
    var form = document.getElementById("addcateg");
    form.action = submitStr;
    form.submit();
}