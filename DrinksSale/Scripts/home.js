function InsertCoin(id, value) {
    var v = parseInt($('#inserted').text());
    v = v + value;
    $('#inserted').text(v);

    console.log('insert coin');
    var url = "/Home/InsertCoin/" + id;
    $.ajax({
        url: url,
        type: 'get',
        dataType: 'json'
    });
}

function AddOrder(id) {
    var order = $('#order-list .order[value="' + id + '"]');
    var product = $('.product-list .product-box[value="' + id + '"]');

    var cost = parseInt(product.find('.value').text());
    var result = parseInt($('.cart .result .value').text());
    result += cost;
    var inserted = parseInt($('#inserted').text());
    if (inserted < result)
        return;

    var count = parseInt(product.find('.count').text());
    if (count === 0)
        return;

    count--;
    product.find('.count').text(count);

    $('.cart .result .value').text(result);
    if (order.length === 0) {
        var name = product.find('.name').text();

        var a = $('#templates .order').clone();
        a.attr("value", id);
        a.find('.name').text(name);
        a.find('.multiplier').text("1");
        a.click(RemoveOrder);
        $('#order-list').append(a);
    }
    else {
        var mult = order.find('.multiplier');
        var k = parseInt(mult.text());

        k++;
        mult.text(k);
        mult.addClass("active");
    }
}

function RemoveOrder() {
    var order = $(this).closest('.order')
    var id = order.attr("value");
    var product = $('.product-list .product-box[value="' + id + '"]');
    var cost = parseInt(product.find('.value').text());
    var result = parseInt($('.cart .result .value').text());

    var mult = order.find('.multiplier');
    var k = parseInt(mult.text());

    result -= k * cost;
    $('.cart .result .value').text(result);

    var count = parseInt(product.find('.count').text());
    count += k;
    product.find('.count').text(count);

    order.remove();
}

function Buy() {
    console.log("buy");
    var orders = $('#order-list .order');
    var data = [];
    orders.each(function (i, elem) {
        console.log(elem);
        var id = $(elem).attr("value");
        var number = $(elem).find(".multiplier").text();
        data.push({ id: id, number: number });
    });

    var url = "/Home/Buy";
    $.ajax({
        url: url,
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(data),
        dataType: 'json',
        success: function (data) {
            if (data === null)
                return;

            $('#inserted').text(0);
            $('#order-list .order').remove();
            $('#delivery-list .delivery').remove();
            $('.cart .result .value').text("0");
            $.each(data.Products, function (i, elem) {
                var a = $('#templates .delivery').clone();
                a.addClass('product');
                a.find('.name').text(elem.Name);
                a.find('.multiplier').text(elem.Count);
                if (elem.Count > 1) {
                    a.find('.multiplier').addClass('active');
                }
                $('#delivery-list').append(a);
            });
            $.each(data.Coins, function (i, elem) {
                var a = $('#templates .delivery').clone();
                a.addClass('coin');
                a.find('.name').text(elem.Name);
                a.find('.multiplier').text(elem.Count);
                console.log("count " + elem.Count);
                if (elem.Count > 1) {
                    a.find('.multiplier').addClass('active');
                }
                $('#delivery-list').append(a);
            });
        }
    });
}