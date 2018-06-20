


class CommentList extends React.Component {
  render() {
      var commentNodes = this.props.data.map(comment => (
     <Comment Author={comment.Author} key={comment.Id}>
        {comment.Text}
      </Comment>
    ));
    return (
      <div className="commentList">
        {commentNodes}
      </div>
    );
  }
}


class Comment extends React.Component {
    rawMarkup() {
    var md = new Remarkable();
    var rawMarkup = md.render(this.props.children.toString());
    return { __html: rawMarkup };
    }

  render() {
    return (
      <div className="comment">
        <h2 className="commentAuthor">
          {this.props.Author}
        </h2>
        <span dangerouslySetInnerHTML={this.rawMarkup()} />
      </div>
    );
  }
}





class CommentForm extends React.Component {
  render() {
    return (
      <div className="commentForm">
        Hello, world! I am a CommentForm.
      </div>
    );
  }
}





class CommentBox extends React.Component {
  //var th = this;

  constructor(props) {
    super(props);
    this.state = {data: []};
  }

  loadCommentsFromServer() {

    const xhr = new XMLHttpRequest();
    xhr.open('get', this.props.url, true);
    xhr.onload = () => {
      const data = JSON.parse(xhr.responseText);
      //this.setState({ data: data });

      //if (this && this._isMounted) {
      //TODO BEN this is a problem, the component isnt mounted, so the state never gets set
         //th.setState({ data: data });
       //  th.state.data = data;
       this.state.data = data;
       //this.forceUpdate()
      //}
      //return data;
    };
    xhr.send();
    //return xhr.response;
  }



  componentDidMount() {
     this.loadCommentsFromServer();
    window.setInterval(() => this.loadCommentsFromServer(), 2001);
    this.forceUpdate();
  }
  render() {
    return (
      <div className="commentBox">
        <h1>Comments</h1>
        <CommentList data={this.state.data} />
        <CommentForm />
      </div>
    );
  }
}

ReactDOM.render(
  <CommentBox url="/comments" pollInterval={2000} />,
  document.getElementById('content')
);
