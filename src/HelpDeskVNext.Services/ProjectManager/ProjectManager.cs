using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrelloNet;

namespace HelpDeskVNext.Services.ProjectManager
{
    public class ProjectManager : IProjectManager
    {
        private readonly ITrello _trello;

        public ProjectManager(ITrello trello)
        {
            _trello = trello;
        }

        public void AddTask()
        {
            var t1 = _trello.Boards.ForMe();
            var board = _trello.Boards.WithId("564b3c80b49d52781bb2dd1f");
            var lists = _trello.Lists.ForBoard(board);

            _trello.Cards.Add(new NewCard("Initial", lists.First()));
        }

        public void UpdateTask()
        {
            throw new NotImplementedException();
        }
    }
}
