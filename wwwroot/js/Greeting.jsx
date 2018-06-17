class CommentList extends React.Component {
  render() {
    return (
      <div className="commentList">
        Hello, world! I am a CommentList.
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
  render() {
    return (
      <div className="commentBox">
        Welcome to the Volunteer App!  This is rendered within React.JS!
        <h1>Comments</h1>
        <CommentList />
        <CommentForm />
      </div>

    );
  }
}



ReactDOM.render(
  <CommentBox />,
  document.getElementById('content')
);
