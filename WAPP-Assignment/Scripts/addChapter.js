//const updateData = (ed) => {
//    $("#dataField").val(ed.getData())
//}

//DecoupledEditor
//    .create(document.querySelector('#editor'), {
//        mediaEmbed: {
//            previewsInData: true
//        },
//    } )
//    .then( editor => {
//        const toolbarContainer = document.querySelector( '#toolbar-container' );
//        toolbarContainer.appendChild( editor.ui.view.toolbar.element );
//        editor.model.document.on('change:data', (evt, data) => {
//            updateData(editor)
//        })
//    } )
//    .catch( error => {
//        console.error( error );
//    } );


//CKEDITOR.plugins.addExternal('codesnippet', '/ckeditor/plugins/codesnippet/')
//CKEDITOR.plugins.addExternal('embedsemantic', '/ckeditor/plugins/embedsemantic/')
CKEDITOR.replace('EditorTxtBox', {
    allowedContent: true,
    embed_provider: '//ckeditor.iframe.ly/api/oembed?url={url}&callback={callback}',
    //extraPlugins: 'codesnippet,embedsemantic',
})
