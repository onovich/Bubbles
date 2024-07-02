namespace Bubbles {

    public class IDRecordService {

        int bubbleEntityID;

        public IDRecordService() { }

        public int PickBubbleEntityID() {
            return ++bubbleEntityID;
        }

        public void Reset() {
            bubbleEntityID = 0;
        }
    }

}