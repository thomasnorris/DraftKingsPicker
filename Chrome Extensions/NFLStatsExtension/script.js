
$(document).ready(() => {
    debugger;

    $('.img-responsive').remove();

    chrome.storage.local.get(['data'], (result) => {
        if (result.data) {
            generateCSV(result.data);
        }
        else {
            var btn = $('<button id="parse">GENERATE CSV</button>').css('font-size', '24pt');
            btn.css('background', 'green');
            btn.insertBefore('table');

            btn.click(generateCSV);
        }
    });
});

async function generateCSV(prev_data = []) {
    var new_data = [];

    var table = $('.d3-o-player-stats--detailed tbody');
    var rows = table.children('tr');

    rows.each((i, row) => {
        var tds = $(row).children('td');
        var row_data = [];

        tds.each((j, td) => {
            var text = $(td).text().trim();
            row_data.push(text);
        });
        new_data.push(row_data);
    });

    if (!prev_data.length)
        prev_data = new_data;
    else
        prev_data = prev_data.concat(new_data);

    chrome.storage.local.set({data: prev_data}, () => {
        var next = $('.nfl-o-table-pagination__next').attr('href');
        if (next) {
            // do next
            window.location = next;
        }
        else {
            chrome.storage.local.clear(() => {
                var type = $('.d3-o-tabs__list-item.d3-is-active').text().trim().replace(' ', '-');
                saveCSV(prev_data, 'NFLStats-' + type + '.csv');
            });
        }
    });
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