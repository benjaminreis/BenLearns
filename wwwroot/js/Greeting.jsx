








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
  constructor(props) {
    super(props);
    this.state = { data: [] };
  }
  loadCommentsFromServer(parentComponent) {
    const xhr = new XMLHttpRequest();
    xhr.open('get', this.props.url, true);
    xhr.onload = () => {
      const data = JSON.parse(xhr.responseText);
      parentComponent.setState({ data: data });
    };
    xhr.send();
  }
  componentDidMount() {
    this.loadCommentsFromServer(this);
    window.setInterval(() => this.loadCommentsFromServer(this), this.props.pollInterval);
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

ReactDOM.render(
  <CommentBox url="/comments" pollInterval={2000} data="[]" />,
  document.getElementById('content')
);
