
$(document).ready(() => {
    var btn = $('<button id="parse">GENERATE CSV</button>').css('font-size', '24pt');
    btn.css('background', 'green');
    btn.insertBefore('table');

    btn.click(generateCSV);
});

function generateCSV(e) {
    debugger;
    var all_data = [];

    var table = $('.sharp tbody');
    var rows = table.children('tr');

    rows.each((i, row) => {
        if (i < 2)
            return;

        var tds = $(row).children('td');
        var row_data = [];
        tds.each((j, td) => {
            var text = $(td).text().trim();

            if (text.includes('$'))
                text = text.replace('$', '').trim();

            if (text.includes(',')) {
                var arr = text.split(',');
                var new_arr = [];
                new_arr[0] = arr[1];
                new_arr[1] = arr[0];
                text = new_arr.join(' ').trim();
            }

            row_data.push(text);
        });

        all_data.push(row_data);
    });

    saveCSV(all_data, 'DraftKingsData.csv');
}

function saveCSV(data, name) {
    data = data.join('\n');

    var content_type = 'text/csv';
    var file = new Blob([data],{
        type: content_type
    });

    var a = document.createElement('a');
    a.download = name;
    a.href = window.URL.createObjectURL(file);
    a.textContent = 'Download';
    a.dataset.downloadurl = [content_type, a.download, a.href].join(':');

    document.body.appendChild(a);

    a.click();
}