
(function($) {
    "use strict"

    //example 1
    var table = $('#example').DataTable({
        order: [],
        createdRow: function ( row, data, index ) {
           $(row).addClass('selected')
        }
    });

    // Add click event listener to table rows
    table.on('click', 'tbody tr', function () {
        // Find the expandable row element in this row
        var $expandableRow = $(this).find('.expandable-row');

        // Toggle the 'expanded' class on the expandable row element
        $expandableRow.toggleClass('expanded');
    });

    // Remove the 'selected' class from all rows on initialization
    table.rows().every(function () {
        this.nodes().to$().removeClass('selected')
    });

    // Set the width of the expandable row container to match the row width
    table.on('draw.dt', function () {
        table.rows().every(function () {
            var $row = $(this.node());
            var $expandableRow = $row.find('.expandable-row');
            $expandableRow.width($row.width());
        });
    });
      
    //table.on('click', 'tbody tr', function() {
    //var $row = table.row(this).nodes().to$();
    //var hasClass = $row.hasClass('selected');
    //if (hasClass) {
    //    $row.removeClass('selected')
    //} else {
    //    $row.addClass('selected')
    //}
    //})
    
    //table.rows().every(function() {
    //this.nodes().to$().removeClass('selected')
    //});


    //example 2
    var table2 = $('#example2').DataTable( {
        createdRow: function ( row, data, index ) {
            $(row).addClass('selected')
        },

        "scrollY":        "42vh",
        "scrollCollapse": true,
        "paging":         false
    });

    table2.on('click', 'tbody tr', function() {
        var $row = table2.row(this).nodes().to$();
        var hasClass = $row.hasClass('selected');
        if (hasClass) {
            $row.removeClass('selected')
        } else {
            $row.addClass('selected')
        }
    })
        
    table2.rows().every(function() {
        this.nodes().to$().removeClass('selected')
    });
   
})(jQuery);