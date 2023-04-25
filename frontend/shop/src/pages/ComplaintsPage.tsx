import ComplaintItem from "../components/ComplaintItem";

function ComplaintsPage  ()  {  
  
  return (
    <div className="flex flex-col items-center justify-center h-screen w-5/6 p-4 bg-gray-300">
        <div className="h-full overflow-y-auto w-full">
            <ComplaintItem></ComplaintItem>
            <ComplaintItem></ComplaintItem>
            <ComplaintItem></ComplaintItem>
        </div>
    </div>
    )
};

export default ComplaintsPage;