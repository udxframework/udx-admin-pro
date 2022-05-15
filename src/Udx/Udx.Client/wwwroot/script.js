


export function InitGantt (id, tasks) {
  var gantt = new Gantt("#" + id, tasks, {
    on_click: function (task) {
      console.log(task);
    },
    view_mode: 'Day',
    language: 'zh'
  });
  return gantt;
}


export function saveAsFile(filename, bytesBase64) {
    var link = document.createElement('a');
    link.download = filename;
    link.href = "data:application/octet-stream;base64," + bytesBase64;
    document.body.appendChild(link); // Needed for Firefox
    link.click();
    document.body.removeChild(link);
}



