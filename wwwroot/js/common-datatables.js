// Function to initialize a DataTable with common settings and specific configurations
function initializeDataTable(tableSelector, ajaxUrl, columnDefs, options = {}) {
    const defaultOptions = {
        processing: true,
        serverSide: false, // Default to client-side unless overridden
        ajax: {
            url: ajaxUrl,
            type: "GET",
            dataType: "json",
            dataSrc: "data", // Standard assumption
            error: function (xhr, status, error) {
                console.error("DataTable AJAX error:", status, error, xhr.responseText);
                // Optionally show a user-friendly error message on the page
                showAlert(`Error loading data: ${xhr.statusText || error}`, 'danger');
            }
        },
        columns: columnDefs,
        // Add other common defaults like maybe language, lengthMenu, etc.
        // Example for dark theme attempt:
        drawCallback: function (settings) {
            // Apply styling after draw if needed
        },
        initComplete: function (settings, json) {
            // Apply dark theme adjustments if needed after table is drawn
            $(tableSelector).closest('.dataTables_wrapper').addClass('dt-dark-theme'); // Example class
        },
        // Add dark theme language options if desired
        language: {
            search: "_INPUT_", // Simple search box
            searchPlaceholder: "Search...",
            lengthMenu: "_MENU_",
            paginate: {
                first: "<<",
                last: ">>",
                next: ">",
                previous: "<"
            },
            // Add other translations if needed
        }
    };

    // Merge custom options provided with defaults
    const finalOptions = $.extend(true, {}, defaultOptions, options);

    return $(tableSelector).DataTable(finalOptions);
}