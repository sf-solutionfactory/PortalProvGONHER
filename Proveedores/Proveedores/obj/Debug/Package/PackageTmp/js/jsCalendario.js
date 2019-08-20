
window.onload = function () {
    new JsDatePick({
        useMode: 2,
        target: "ContentPlaceHolder1_txtInicioVigencia",
            dateFormat: "%d-%m-%Y"
    });

    new JsDatePick({
        useMode: 2,
        target: "ContentPlaceHolder1_txtFinVigencia",
        dateFormat: "%d-%m-%Y"
    });

  
};
