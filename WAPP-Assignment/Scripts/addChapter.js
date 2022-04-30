const updateData = (ed) => {
    $("#dataField").val(ed.getData())
}

DecoupledEditor
    .create(document.querySelector('#editor'), {
        mediaEmbed: {
            previewsInData: true
        },
    } )
    .then( editor => {
        const toolbarContainer = document.querySelector( '#toolbar-container' );
        toolbarContainer.appendChild( editor.ui.view.toolbar.element );
        editor.model.document.on('change:data', (evt, data) => {
            updateData(editor)
        })
    } )
    .catch( error => {
        console.error( error );
    } );
